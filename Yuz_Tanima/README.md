## Yüz Tanıma (WinForms + EmguCV)

Windows Forms (.NET 8) üzerinde EmguCV kullanarak kamera önizlemesi, yüz algılama, örnek kaydı, LBPH ile eğitim ve canlı tanıma yapan basit bir uygulama.

### Özellikler
- Kamera önizleme (1280x720, gerekirse 640x480’e düşer)
- Yüz algılama: Haar (frontalface_default + alt2) ve yedek olarak DNN (Res10-SSD 300x300)
- Yüz örneği kaydı: 100x100 gri, `data/faces/eren/`
- Model eğitimi: LBPH, model `data/models/eren.yml`
- Canlı tanıma: etiket + mesafe (confidence)
- (İsteğe bağlı) Sesli geri bildirim: “Hoş geldin eren” (`SPEECH` koşullu sembolü ile)

### Proje Yapısı
- `yuz_tanima/`
  - `Form1.cs`: Kamera, algılama, kayıt, eğitim ve tanıma akışı
  - `Form1.Designer.cs`: Arayüz bileşenleri (PictureBox + butonlar + status)
  - `Program.cs`: WinForms başlangıcı
  - `yuz_tanima.csproj`: .NET 8 Windows Desktop, EmguCV bağımlılıkları
  - `data/`
    - `haarcascades/` → Haar XML’leri otomatik indirilir
    - `dnn/` → DNN model/prototxt otomatik indirilir
    - `faces/eren/` → Kayıtlı 100x100 gri yüz örnekleri
    - `models/` → LBPH model (`eren.yml`)

### Bağımlılıklar
- NuGet: `Emgu.CV`, `Emgu.CV.runtime.windows`, `Emgu.CV.UI`
- (Opsiyonel) `System.Speech` (Windows Desktop hedefleme paketi kuruluysa)

### Çalıştırma
1. Visual Studio ile `yuz_tanima.sln` açın.
2. Configuration: Debug, Platform: x64.
3. Build > Rebuild Solution, ardından F5.
4. Uygulamada:
   - “Kamera Başlat” → önizleme + algılama başlar.
   - “Yüz Kaydet” → yeşil kutu varken örnekleri `data/faces/eren/`’e kaydeder (100x100 gri).
   - “Eğit / Tanı” → LBPH eğitilip `data/models/eren.yml` kaydedilir; canlı tanıma başlar.

### Notlar ve İpuçları
- Haar algılama ışık/açıya duyarlıdır; olmadığında DNN fallback devreye girer.
- Confidence eşiği LBPH için varsayılan 75’tir; sonuçlara göre ayarlanabilir.
- Sesli geri bildirim için proje Build ayarlarında `Conditional compilation symbols` bölümüne `SPEECH` ekleyin ve Windows Desktop hedefleme bileşenlerinin kurulu olduğundan emin olun.

### Güvenli Kapanış
- Form kapanışında kamera (`VideoCapture`) ve kaynaklar dispose edilir.

### Lisans / Atıf
- DNN yüz modeli: OpenCV’nin örnek yüz tespit modeli (Res10 300x300 SSD Caffe).
- Haar XML’leri: OpenCV `haarcascades` koleksiyonu.

---
Bu proje bir okul proje ödevidir.


