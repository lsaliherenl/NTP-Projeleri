using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.IO;
using System.Linq;
using Emgu.CV.Face;
#if SPEECH
using System.Speech.Synthesis;
#endif
using System.Net.Http;
using Emgu.CV.Util;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.Dnn;

namespace yuz_tanima
{
    public partial class Form1 : Form
    {
        private VideoCapture? capture;
        private System.Windows.Forms.Timer? frameTimer;
        private bool isPreviewing = false;
        private CascadeClassifier? faceCascade;
        private CascadeClassifier? altFaceCascade;
        private System.Drawing.Rectangle? lastFaceRect;
        private Mat? lastGrayFrame;
        private LBPHFaceRecognizer? recognizer;
        private Net? dnnFaceNet;
#if SPEECH
        private SpeechSynthesizer? synthesizer;
        private DateTime lastSpeakUtc = DateTime.MinValue;
#endif

        public Form1()
        {
            InitializeComponent();
            WireUpEvents();
            InitializeTimers();

#if SPEECH
            synthesizer = new SpeechSynthesizer();
#endif
        }

        private void WireUpEvents()
        {
            btnStartCamera.Click += BtnStartCamera_Click;
            btnSaveFace.Click += BtnSaveFace_Click;
            btnTrainRecognize.Click += BtnTrainRecognize_Click;
        }

        private void InitializeTimers()
        {
            frameTimer = new System.Windows.Forms.Timer();
            frameTimer.Interval = 33; // ~30 FPS
            frameTimer.Tick += FrameTimer_Tick;
        }

        private void BtnStartCamera_Click(object? sender, EventArgs e)
        {
            if (isPreviewing)
            {
                StopPreview();
                return;
            }

            try
            {
                InitializeCapture();
                if (capture == null || !capture.IsOpened)
                {
                    toolStripStatusLabel1.Text = "Kamera açılamadı";
                    return;
                }

                // Haar cascade dosyasını garantile ve yükle
                try
                {
                    var mainPath = EnsureCascadeAvailable();
                    // Alternatif cascade'i hazırla (alt2)
                    var cascDir = Path.Combine(AppContext.BaseDirectory, "data", "haarcascades");
                    var altPath = Path.Combine(cascDir, "haarcascade_frontalface_alt2.xml");
                    if (!File.Exists(altPath) || new FileInfo(altPath).Length <= 1024)
                    {
                        try
                        {
                            var urlAlt = "https://raw.githubusercontent.com/opencv/opencv/4.x/data/haarcascades/haarcascade_frontalface_alt2.xml";
                            using var httpAlt = new HttpClient();
                            var bytesAlt = httpAlt.GetByteArrayAsync(urlAlt).GetAwaiter().GetResult();
                            File.WriteAllBytes(altPath, bytesAlt);
                        }
                        catch { }
                    }

                    if (!string.IsNullOrEmpty(mainPath) && File.Exists(mainPath))
                    {
                        faceCascade?.Dispose();
                        faceCascade = new CascadeClassifier(mainPath);
                        if (File.Exists(altPath))
                        {
                            altFaceCascade?.Dispose();
                            altFaceCascade = new CascadeClassifier(altPath);
                        }
                        toolStripStatusLabel1.Text = "Kamera çalışıyor (cascade yüklendi)";
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Kamera çalışıyor (cascade bulunamadı)";
                    }
                }
                catch (Exception cex)
                {
                    toolStripStatusLabel1.Text = $"Cascade hata: {cex.Message}";
                }

                isPreviewing = true;
                btnStartCamera.Text = "Durdur";
                toolStripStatusLabel1.Text = "Kamera çalışıyor";
                frameTimer?.Start();
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = $"Kamera hata: {ex.Message}";
                StopPreview();
            }
        }

        private string EnsureCascadeAvailable()
        {
            var cascDir = Path.Combine(AppContext.BaseDirectory, "data", "haarcascades");
            Directory.CreateDirectory(cascDir);
            var cascadePath = Path.Combine(cascDir, "haarcascade_frontalface_default.xml");
            if (File.Exists(cascadePath) && new FileInfo(cascadePath).Length > 1024)
            {
                return cascadePath;
            }

            // Yoksa indirmeyi dene
            var url = "https://raw.githubusercontent.com/opencv/opencv/4.x/data/haarcascades/haarcascade_frontalface_default.xml";
            using var http = new HttpClient();
            var bytes = http.GetByteArrayAsync(url).GetAwaiter().GetResult();
            File.WriteAllBytes(cascadePath, bytes);
            return cascadePath;
        }

        private void EnsureAltCascadeAvailable()
        {
            var cascDir = Path.Combine(AppContext.BaseDirectory, "data", "haarcascades");
            Directory.CreateDirectory(cascDir);
            var altPath = Path.Combine(cascDir, "haarcascade_frontalface_alt2.xml");
            if (File.Exists(altPath) && new FileInfo(altPath).Length > 1024) return;
            try
            {
                var urlAlt = "https://raw.githubusercontent.com/opencv/opencv/4.x/data/haarcascades/haarcascade_frontalface_alt2.xml";
                using var httpAlt = new HttpClient();
                var bytesAlt = httpAlt.GetByteArrayAsync(urlAlt).GetAwaiter().GetResult();
                File.WriteAllBytes(altPath, bytesAlt);
            }
            catch { }
        }

        private void EnsureDnnModelAvailable()
        {
            var dnnDir = Path.Combine(AppContext.BaseDirectory, "data", "dnn");
            Directory.CreateDirectory(dnnDir);
            var proto = Path.Combine(dnnDir, "deploy.prototxt");
            var model = Path.Combine(dnnDir, "res10_300x300_ssd_iter_140000.caffemodel");
            if (!File.Exists(proto) || new FileInfo(proto).Length < 1000)
            {
                var urlP = "https://raw.githubusercontent.com/opencv/opencv/master/samples/dnn/face_detector/deploy.prototxt";
                using var http = new HttpClient();
                File.WriteAllBytes(proto, http.GetByteArrayAsync(urlP).GetAwaiter().GetResult());
            }
            if (!File.Exists(model) || new FileInfo(model).Length < 1000000)
            {
                var urlM = "https://raw.githubusercontent.com/opencv/opencv_3rdparty/dnn_samples_face_detector_20170830/res10_300x300_ssd_iter_140000.caffemodel";
                using var http = new HttpClient();
                File.WriteAllBytes(model, http.GetByteArrayAsync(urlM).GetAwaiter().GetResult());
            }
            try
            {
                dnnFaceNet ??= DnnInvoke.ReadNetFromCaffe(proto, model);
            }
            catch { }
        }

        private System.Drawing.Rectangle[] DetectFacesWithDnn(Mat bgrFrame)
        {
            if (dnnFaceNet == null || bgrFrame == null || bgrFrame.IsEmpty) return Array.Empty<System.Drawing.Rectangle>();
            using var blob = DnnInvoke.BlobFromImage(bgrFrame, 1.0, new Size(300, 300), new MCvScalar(104, 177, 123), false, false);
            dnnFaceNet.SetInput(blob, "data");
            using var detections = dnnFaceNet.Forward("detection_out");
            var faces = new System.Collections.Generic.List<System.Drawing.Rectangle>();
            if (detections.IsEmpty) return faces.ToArray();
            int w = bgrFrame.Width, h = bgrFrame.Height;
            var dataObj = detections.GetData();
            if (dataObj is float[] arr)
            {
                int count = arr.Length / 7;
                for (int i = 0; i < count; i++)
                {
                    float confidence = arr[i * 7 + 2];
                    if (confidence < 0.6f) continue;
                    int x1 = (int)(arr[i * 7 + 3] * w);
                    int y1 = (int)(arr[i * 7 + 4] * h);
                    int x2 = (int)(arr[i * 7 + 5] * w);
                    int y2 = (int)(arr[i * 7 + 6] * h);
                    var rect = Rectangle.FromLTRB(Math.Max(0, x1), Math.Max(0, y1), Math.Min(w - 1, x2), Math.Min(h - 1, y2));
                    if (rect.Width > 20 && rect.Height > 20) faces.Add(rect);
                }
            }
            return faces.ToArray();
        }

        private void FrameTimer_Tick(object? sender, EventArgs e)
        {
            if (!isPreviewing || capture == null || !capture.IsOpened)
            {
                return;
            }

            using Mat frame = new Mat();
            bool ok = capture.Read(frame);
            if (!ok || frame.IsEmpty)
            {
                toolStripStatusLabel1.Text = "Frame boş geldi";
                return;
            }

            Mat display = frame.Clone();

            if (faceCascade != null)
            {
                lastGrayFrame?.Dispose();
                lastGrayFrame = new Mat();
                CvInvoke.CvtColor(display, lastGrayFrame, ColorConversion.Bgr2Gray);
                CvInvoke.EqualizeHist(lastGrayFrame, lastGrayFrame);

                if (!lastGrayFrame.IsEmpty)
                {
                    System.Drawing.Rectangle[] faces;
                    try
                    {
                        // Daha toleranslı parametreler; küçük yüzleri yakalamak için min size 30x30
                        faces = faceCascade.DetectMultiScale(
                            lastGrayFrame,
                            1.05,
                            2,
                            new System.Drawing.Size(30, 30),
                            System.Drawing.Size.Empty);

                        if ((faces == null || faces.Length == 0) && altFaceCascade != null)
                        {
                            faces = altFaceCascade.DetectMultiScale(
                                lastGrayFrame,
                                1.05,
                                2,
                                new System.Drawing.Size(30, 30),
                                System.Drawing.Size.Empty);
                        }

                        if (faces == null || faces.Length == 0)
                        {
                            faces = DetectFacesWithDnn(frame);
                        }
                    }
                    catch
                    {
                        // Fallback: OpenCV varsayılan algılama ile bir kez daha dene
                        try
                        {
                            faces = faceCascade.DetectMultiScale(lastGrayFrame);
                        }
                        catch
                        {
                            toolStripStatusLabel1.Text = "Yüz algılanamadı (cascade)";
                            faces = Array.Empty<System.Drawing.Rectangle>();
                        }
                    }

                    lastFaceRect = faces.FirstOrDefault();

                    foreach (var rect in faces)
                    {
                        CvInvoke.Rectangle(display, rect, new MCvScalar(0, 255, 0), 2);

                        if (recognizer != null && lastGrayFrame != null)
                        {
                            using var roi = new Mat(lastGrayFrame, rect);
                            using var resized = new Mat();
                            CvInvoke.Resize(roi, resized, new System.Drawing.Size(100, 100));

                            var result = recognizer.Predict(resized);
                            bool isRecognized = result.Label == 1 && result.Distance <= 75;
                            string name = isRecognized ? "eren" : "Bilinmeyen";
                            string text = $"{name} ({result.Distance:F1})";
                            CvInvoke.PutText(
                                display,
                                text,
                                new System.Drawing.Point(rect.X, rect.Y - 8 < 0 ? rect.Y + rect.Height + 20 : rect.Y - 8),
                                FontFace.HersheySimplex,
                                0.6,
                                isRecognized ? new MCvScalar(0, 255, 0) : new MCvScalar(0, 69, 255),
                                2);

#if SPEECH
                        if (isRecognized && synthesizer != null)
                        {
                            var now = DateTime.UtcNow;
                            if ((now - lastSpeakUtc).TotalSeconds > 5)
                            {
                                lastSpeakUtc = now;
                                try { synthesizer.SpeakAsyncCancelAll(); synthesizer.SpeakAsync("Hoş geldin eren"); } catch { }
                            }
                        }
#endif
                    }
                }
            }

            }

            using Bitmap bmp = display.ToBitmap();
            var old = pictureBoxPreview.Image;
            pictureBoxPreview.Image = (Bitmap)bmp.Clone();
            old?.Dispose();
        }

        private void InitializeCapture()
        {
            try
            {
                // Var olanı kapat
                if (capture != null)
                {
                    capture.Dispose();
                    capture = null;
                }

                // Windows'ta DShow ve Any backendlerini sırayla dene
                var apis = new[] { VideoCapture.API.DShow, VideoCapture.API.Any };
                foreach (var api in apis)
                {
                    try
                    {
                        var cap = new VideoCapture(0, api);
                        if (!cap.IsOpened) { cap.Dispose(); continue; }

                        // MJPG fourcc deneyerek boş kare sorununu azalt
                        try { cap.Set(CapProp.FourCC, VideoWriter.Fourcc('M','J','P','G')); } catch { }

                        // Önce 1280x720 dene
                        cap.Set(CapProp.FrameWidth, 1280);
                        cap.Set(CapProp.FrameHeight, 720);

                        bool ok = false;
                        using (var test = new Mat())
                        {
                            ok = cap.Read(test) && !test.IsEmpty;
                            if (!ok)
                            {
                                // 640x480'e geri düş
                                cap.Set(CapProp.FrameWidth, 640);
                                cap.Set(CapProp.FrameHeight, 480);
                                ok = cap.Read(test) && !test.IsEmpty;
                            }
                        }

                        if (ok)
                        {
                            capture = cap;
                            toolStripStatusLabel1.Text = $"Kamera açıldı ({api})";
                            break;
                        }
                        cap.Dispose();
                    }
                    catch { /* bir sonraki backend */ }
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = $"Kamera hata: {ex.Message}";
            }
        }

        private void StopPreview()
        {
            frameTimer?.Stop();
            isPreviewing = false;
            btnStartCamera.Text = "Kamera Başlat";
            toolStripStatusLabel1.Text = "Durdu";

            if (capture != null)
            {
                capture.Dispose();
                capture = null;
            }

            faceCascade?.Dispose();
            faceCascade = null;
            lastGrayFrame?.Dispose();
            lastGrayFrame = null;
            lastFaceRect = null;
        }

        private void BtnSaveFace_Click(object? sender, EventArgs e)
        {
            try
            {
                if (lastGrayFrame == null || lastGrayFrame.IsEmpty || lastFaceRect == null || lastFaceRect.Value.Width == 0)
                {
                    toolStripStatusLabel1.Text = "Önce yeşil kutu içinde yüz algılanmalı";
                    return;
                }

                var rect = lastFaceRect.Value;
                rect.Intersect(new System.Drawing.Rectangle(0, 0, lastGrayFrame.Width, lastGrayFrame.Height));
                if (rect.Width <= 0 || rect.Height <= 0)
                {
                    toolStripStatusLabel1.Text = "Yüz ROI geçersiz";
                    return;
                }

                using Mat faceRoi = new Mat(lastGrayFrame, rect);
                using Mat resized = new Mat();
                CvInvoke.Resize(faceRoi, resized, new System.Drawing.Size(100, 100), 0, 0, Inter.Area);

                var facesDir = Path.Combine(AppContext.BaseDirectory, "data", "faces", "eren");
                Directory.CreateDirectory(facesDir);

                int nextIdx = Directory.EnumerateFiles(facesDir, "img_*.png").Count() + 1;
                var filePath = Path.Combine(facesDir, $"img_{nextIdx:D4}.png");
                resized.Save(filePath);
                toolStripStatusLabel1.Text = $"Kaydedildi: {Path.GetFileName(filePath)}";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = $"Kayıt hata: {ex.Message}";
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            frameTimer?.Stop();
            frameTimer?.Dispose();
            StopPreview();
            base.OnFormClosing(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            TryLoadModel();
        }

        private void TryLoadModel()
        {
            try
            {
                var modelPath = Path.Combine(AppContext.BaseDirectory, "data", "models", "eren.yml");
                if (File.Exists(modelPath))
                {
                    recognizer ??= new LBPHFaceRecognizer(1, 8, 8, 8, 75);
                    recognizer.Read(modelPath);
                    toolStripStatusLabel1.Text = "Model yüklendi";
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = $"Model yükleme hata: {ex.Message}";
            }
        }

        private void BtnTrainRecognize_Click(object? sender, EventArgs e)
        {
            try
            {
                var facesDir = Path.Combine(AppContext.BaseDirectory, "data", "faces", "eren");
                if (!Directory.Exists(facesDir))
                {
                    toolStripStatusLabel1.Text = "Yüz örneği bulunamadı";
                    return;
                }

                var files = Directory.EnumerateFiles(facesDir, "*.png").OrderBy(p => p).ToArray();
                if (files.Length == 0)
                {
                    toolStripStatusLabel1.Text = "Örnek yok";
                    return;
                }

                using var vecImages = new VectorOfMat();
                using var vecLabels = new VectorOfInt();
                foreach (var file in files)
                {
                    using Mat img = CvInvoke.Imread(file, ImreadModes.Grayscale);
                    vecImages.Push(img);
                    vecLabels.Push(new int[] { 1 }); // eren = 1
                }

                recognizer?.Dispose();
                recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 75);
                recognizer.Train(vecImages, vecLabels);

                var modelDir = Path.Combine(AppContext.BaseDirectory, "data", "models");
                Directory.CreateDirectory(modelDir);
                var modelPath = Path.Combine(modelDir, "eren.yml");
                recognizer.Write(modelPath);

                toolStripStatusLabel1.Text = $"Eğitim tamamlandı ({files.Length} örnek).";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = $"Eğitim hata: {ex.Message}";
            }
        }

        private void pictureBoxPreview_Click(object sender, EventArgs e)
        {

        }
    }
}

