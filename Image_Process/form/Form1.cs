using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ImageSharpImage = SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>;

namespace form
{
    public partial class Form1 : Form
    {
        ImageSharpImage? loadedImage;
        ImageSharpImage? processedImage;
        readonly Stack<ImageSharpImage> undoStack = new();
        readonly Stack<ImageSharpImage> redoStack = new();
        float zoomFactor = 1f;

        public Form1()
        {
            InitializeComponent();
            btnDownload.Click += btnDownload_Click;
            AllowDrop = true;
            DragEnter += Form1_DragEnter;
            DragDrop += Form1_DragDrop;
            KeyPreview = true;
            KeyDown += Form1_KeyDown;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
        }

        private void btnLoad_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.png;*.jpeg;*.bmp"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadImage(ofd.FileName);
            }
        }

        private void btnGray_Click(object? sender, EventArgs e)
        {
            ApplyAndRender(img => img.Clone(x => x.Grayscale()));
        }

        private void btnBlur_Click(object? sender, EventArgs e)
        {
            ApplyAndRender(img => img.Clone(x => x.GaussianBlur(5)));
        }

        private void btnSepia_Click(object? sender, EventArgs e)
        {
            ApplyAndRender(img => img.Clone(x => x.Sepia()));
        }

        private void btnSharpen_Click(object? sender, EventArgs e)
        {
            ApplyAndRender(img => img.Clone(x => x.GaussianSharpen(3)));
        }

        private void btnDownload_Click(object? sender, EventArgs e)
        {
            if (processedImage == null)
            {
                MessageBox.Show("Önce işlenmiş bir görüntü oluşturun (Gray veya Blur).");
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "PNG|*.png|JPEG|*.jpg;*.jpeg|BMP|*.bmp",
                FileName = "processed.png"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                using var fs = File.Open(sfd.FileName, FileMode.Create, FileAccess.Write);

                if (ext == ".jpg" || ext == ".jpeg")
                {
                    processedImage.SaveAsJpeg(fs);
                }
                else if (ext == ".bmp")
                {
                    processedImage.SaveAsBmp(fs);
                }
                else
                {
                    processedImage.SaveAsPng(fs);
                }
            }
        }

        private void pictureBox1_Click(object? sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object? sender, EventArgs e)
        {
        }

        private void btnRotateLeft_Click(object? sender, EventArgs e)
        {
            ApplyAndRender(img => img.Clone(x => x.Rotate(-90)));
        }

        private void btnRotateRight_Click(object? sender, EventArgs e)
        {
            ApplyAndRender(img => img.Clone(x => x.Rotate(90)));
        }

        private void btnFlipH_Click(object? sender, EventArgs e)
        {
            ApplyAndRender(img => img.Clone(x => x.Flip(FlipMode.Horizontal)));
        }

        private void btnFlipV_Click(object? sender, EventArgs e)
        {
            ApplyAndRender(img => img.Clone(x => x.Flip(FlipMode.Vertical)));
        }

        private void btnReset_Click(object? sender, EventArgs e)
        {
            if (loadedImage == null) return;
            processedImage?.Dispose();
            processedImage = null;
            ClearStacks();
            zoomFactor = 1f;
            RenderOriginal();
            pictureProcessed.Image = null;
        }

        private void btnUndo_Click(object? sender, EventArgs e)
        {
            if (undoStack.Count == 0) return;
            if (GetCurrentImage() is ImageSharpImage current)
            {
                redoStack.Push(current.Clone());
            }
            var prev = undoStack.Pop();
            processedImage?.Dispose();
            processedImage = prev;
            RenderProcessed();
        }

        private void btnRedo_Click(object? sender, EventArgs e)
        {
            if (redoStack.Count == 0) return;
            if (GetCurrentImage() is ImageSharpImage current)
            {
                undoStack.Push(current.Clone());
            }
            var next = redoStack.Pop();
            processedImage?.Dispose();
            processedImage = next;
            RenderProcessed();
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.O)
            {
                btnLoad_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                btnDownload_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                btnUndo_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                btnRedo_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void Form1_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Form1_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
            {
                LoadImage(files[0]);
            }
        }

        private ImageSharpImage? GetCurrentImage()
        {
            return processedImage ?? loadedImage;
        }

        private void ApplyAndRender(Func<ImageSharpImage, ImageSharpImage> operation)
        {
            var current = GetCurrentImage();
            if (current == null)
            {
                MessageBox.Show("Önce bir görsel yükleyin.");
                return;
            }

            // Önce mevcut görüntüyü klonla; dispose işlemlerinden önce güvenceye al.
            var undoSnapshot = current.Clone();
            var nextImage = operation(current.Clone());

            undoStack.Push(undoSnapshot);
            redoStack.Clear();

            processedImage?.Dispose();
            processedImage = nextImage;

            RenderProcessed();
        }

        private void LoadImage(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Dosya bulunamadı.");
                return;
            }

            loadedImage?.Dispose();
            processedImage?.Dispose();
            ClearStacks();
            try
            {
                // ImageSharp ile yükleme
                loadedImage = SixLabors.ImageSharp.Image.Load<Rgba32>(path);
                processedImage = null;
                zoomFactor = 1f;
                RenderOriginal();
                if (pictureProcessed != null)
                {
                    pictureProcessed.Image = null;
                }
            }
            catch (SixLabors.ImageSharp.UnknownImageFormatException ex)
            {
                // ImageSharp başarısız oldu, System.Drawing ile alternatif yükleme dene
                try
                {
                    using (var sysImg = System.Drawing.Image.FromFile(path))
                    {
                        // System.Drawing'dan ImageSharp'a dönüştür
                        using (var ms = new MemoryStream())
                        {
                            sysImg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            ms.Seek(0, SeekOrigin.Begin);
                            loadedImage = SixLabors.ImageSharp.Image.Load<Rgba32>(ms);
                            processedImage = null;
                            zoomFactor = 1f;
                            RenderOriginal();
                            if (pictureProcessed != null)
                            {
                                pictureProcessed.Image = null;
                            }
                            return; // Başarılı, çık
                        }
                    }
                }
                catch (Exception ex2)
                {
                    // Her iki yöntem de başarısız
                    var ext = Path.GetExtension(path).ToLowerInvariant();
                    var supportedFormats = "jpg, png, jpeg, bmp, gif";
                    
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".gif")
                    {
                        MessageBox.Show($"Dosya formatı destekleniyor ancak görsel yüklenemedi.\n\n" +
                            $"Dosya bozuk olabilir veya format uyumsuzluğu var.\n\n" +
                            $"ImageSharp hatası: {ex.Message}\n" +
                            $"Alternatif yükleme hatası: {ex2.Message}", 
                            "Görsel Yükleme Hatası", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Desteklenmeyen format: {ext}\n\n" +
                            $"Desteklenen formatlar: {supportedFormats}", 
                            "Desteklenmeyen Format", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Görsel yüklenemedi.\n\n" +
                    $"Hata: {ex.Message}\n\n" +
                    $"Dosya yolu: {path}", 
                    "Hata", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void ClearStacks()
        {
            foreach (var img in undoStack) img.Dispose();
            foreach (var img in redoStack) img.Dispose();
            undoStack.Clear();
            redoStack.Clear();
        }

        private void RenderOriginal()
        {
            if (loadedImage == null || pictureOriginal == null) return;
            pictureOriginal.Image = ImageToPictureBox(loadedImage, pictureOriginal, zoomFactor);
        }

        private void RenderProcessed()
        {
            if (processedImage == null || pictureProcessed == null) return;
            pictureProcessed.Image = ImageToPictureBox(processedImage, pictureProcessed, zoomFactor);
        }

        private void pictureOriginal_MouseWheel(object? sender, MouseEventArgs e)
        {
            AdjustZoom(e.Delta);
        }

        private void pictureProcessed_MouseWheel(object? sender, MouseEventArgs e)
        {
            AdjustZoom(e.Delta);
        }

        private void pictureBox_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Control c)
            {
                c.Focus();
            }
        }

        private void AdjustZoom(int delta)
        {
            var factor = delta > 0 ? 0.1f : -0.1f;
            zoomFactor = Math.Clamp(zoomFactor + factor, 0.2f, 5f);
            RenderOriginal();
            RenderProcessed();
        }

        // ImageSharp Image → PictureBox Image çevirme (kutunun boyutuna sığacak şekilde, zoom destekli)
        private System.Drawing.Image ImageToPictureBox(ImageSharpImage img, PictureBox box, float factor)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box), "PictureBox kontrolü null olamaz.");
            }

            using (var ms = new MemoryStream())
            {
                // Ön izleme için kutuya sığacak şekilde orantılı yeniden boyutlandır + zoom
                var basisW = box.Parent?.ClientSize.Width > 0 ? box.Parent.ClientSize.Width : box.Width;
                var basisH = box.Parent?.ClientSize.Height > 0 ? box.Parent.ClientSize.Height : box.Height;
                var targetWidth = Math.Max(1, (int)(basisW * factor));
                var targetHeight = Math.Max(1, (int)(basisH * factor));

                var resized = img.Clone(ctx => ctx.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new SixLabors.ImageSharp.Size(targetWidth, targetHeight)
                }));

                resized.SaveAsPng(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return System.Drawing.Image.FromStream(ms);
            }
        }
    }
}
