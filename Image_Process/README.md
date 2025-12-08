# ImageProcess - Görüntü İşleme Uygulaması

Modern ve kullanıcı dostu bir Windows Forms görüntü işleme uygulaması. Görsellerinize çeşitli filtreler uygulayabilir, dönüştürebilir ve profesyonel düzenlemeler yapabilirsiniz.

## 📋 İçindekiler

- [Özellikler](#özellikler)
- [Teknolojiler](#teknolojiler)
- [Gereksinimler](#gereksinimler)
- [Kurulum](#kurulum)
- [Kullanım](#kullanım)
- [Klavye Kısayolları](#klavye-kısayolları)
- [Proje Yapısı](#proje-yapısı)
- [Geliştirme](#geliştirme)
- [Lisans](#lisans)

## ✨ Özellikler

### Görüntü Yükleme
- **Dosya Seçme**: "Görsel Yükle" butonu ile görsel seçebilirsiniz
- **Sürükle-Bırak**: Görselleri doğrudan uygulama penceresine sürükleyip bırakabilirsiniz
- **Çoklu Format Desteği**: JPG, PNG, JPEG, BMP formatları desteklenir
- **Hata Yönetimi**: Bozuk veya desteklenmeyen formatlar için kullanıcı dostu hata mesajları

### Görüntü Filtreleri
- **Gri Tonlama (Gray)**: Görseli siyah-beyaz formata dönüştürür
- **Bulanıklaştırma (Blur)**: Gaussian blur efekti ile görseli yumuşatır
- **Sepya**: Klasik sepya tonu efekti uygular
- **Keskinleştirme (Sharpen)**: Görseli daha net ve keskin hale getirir

### Dönüştürme İşlemleri
- **Sola Döndürme (90°)**: Görseli saat yönünün tersine 90 derece döndürür
- **Sağa Döndürme (90°)**: Görseli saat yönünde 90 derece döndürür
- **Yatay Ayna**: Görseli yatay eksende yansıtır
- **Dikey Ayna**: Görseli dikey eksende yansıtır

### Düzenleme Araçları
- **Undo (Geri Al)**: Son işlemi geri alır
- **Redo (Yinele)**: Geri alınan işlemi tekrar uygular
- **Orijinale Dön**: Tüm işlemleri temizleyip orijinal görsele döner
- **Zoom**: Mouse tekerleği ile görseli yakınlaştırıp uzaklaştırabilirsiniz (0.2x - 5x arası)

### Görüntüleme
- **Yan Yana Karşılaştırma**: Orijinal ve işlenmiş görseli yan yana görüntüleme
- **Otomatik Boyutlandırma**: Görseller otomatik olarak görüntü alanına sığacak şekilde ölçeklenir
- **Zoom Desteği**: Her iki görsel alanında da zoom yapılabilir

### Kaydetme
- **Çoklu Format**: İşlenmiş görseli PNG, JPEG veya BMP formatında kaydedebilirsiniz
- **Özel Dosya Adı**: Kaydetme sırasında istediğiniz dosya adını seçebilirsiniz

## 🛠️ Teknolojiler

- **.NET 10.0**: Modern .NET platformu
- **Windows Forms**: Masaüstü kullanıcı arayüzü
- **SixLabors.ImageSharp 3.1.12**: Yüksek performanslı görüntü işleme kütüphanesi
- **SixLabors.ImageSharp.Drawing 2.1.7**: Görüntü çizim ve manipülasyon araçları
- **Microsoft.ML 5.0.0**: Makine öğrenmesi desteği (gelecek özellikler için)
- **Microsoft.ML.ImageAnalytics 5.0.0**: Görüntü analizi için ML.NET araçları
- **Microsoft.ML.Vision 5.0.0**: Görüntü sınıflandırma için ML.NET araçları

## 📦 Gereksinimler

- **İşletim Sistemi**: Windows 10 veya üzeri
- **.NET Runtime**: .NET 10.0 Runtime
- **RAM**: Minimum 2 GB (büyük görseller için daha fazla önerilir)
- **Disk Alanı**: ~50 MB (uygulama ve bağımlılıklar için)

## 🚀 Kurulum

### Yöntem 1: Hazır Derlenmiş Sürüm

1. `form/bin/Debug/net10.0-windows/` klasörüne gidin
2. `form.exe` dosyasını çalıştırın

### Yöntem 2: Kaynak Koddan Derleme

1. **Gereksinimler**:
   - Visual Studio 2022 veya .NET 10.0 SDK
   - Git (opsiyonel)

2. **Projeyi İndirin**:
   ```bash
   git clone <repository-url>
   cd ImageProcess
   ```

3. **Bağımlılıkları Yükleyin**:
   ```bash
   cd form
   dotnet restore
   ```

4. **Projeyi Derleyin**:
   ```bash
   dotnet build
   ```

5. **Uygulamayı Çalıştırın**:
   ```bash
   dotnet run
   ```

   Veya Visual Studio'da `F5` tuşuna basarak çalıştırabilirsiniz.

## 📖 Kullanım

### Görsel Yükleme

1. **Buton ile**:
   - "Görsel Yükle" butonuna tıklayın
   - Dosya seçim penceresinden bir görsel seçin

2. **Sürükle-Bırak ile**:
   - Görsel dosyasını Windows Explorer'dan sürükleyin
   - Uygulama penceresine bırakın

### Filtre Uygulama

1. Bir görsel yükleyin
2. İstediğiniz filtre butonuna tıklayın:
   - **Gray**: Gri tonlama
   - **Blur**: Bulanıklaştırma
   - **Sepya**: Sepya efekti
   - **Keskinleştir**: Keskinleştirme

3. İşlenmiş görsel sağ taraftaki panelde görüntülenecektir

### Dönüştürme İşlemleri

- **Sol 90°**: Görseli sola döndürür
- **Sağ 90°**: Görseli sağa döndürür
- **Ayna Yatay**: Yatay eksende yansıtır
- **Ayna Dikey**: Dikey eksende yansıtır

### Zoom Kullanımı

- Mouse tekerleğini yukarı kaydırarak yakınlaştırın
- Mouse tekerleğini aşağı kaydırarak uzaklaştırın
- Zoom seviyesi 0.2x ile 5x arasında değişebilir

### İşlenmiş Görseli Kaydetme

1. İstediğiniz filtreleri uygulayın
2. "İndir" butonuna tıklayın
3. Kaydetme penceresinde:
   - Dosya adını belirleyin
   - Format seçin (PNG, JPEG, BMP)
   - Konumu seçin
   - "Kaydet" butonuna tıklayın

### İşlemleri Geri Alma/Yineleme

- **Undo**: Son işlemi geri almak için "Undo" butonuna tıklayın
- **Redo**: Geri alınan işlemi tekrar uygulamak için "Redo" butonuna tıklayın

### Orijinale Dönme

- "Orijinale Dön" butonuna tıklayarak tüm işlemleri temizleyip orijinal görsele dönebilirsiniz

## ⌨️ Klavye Kısayolları

| Kısayol | İşlev |
|---------|-------|
| `Ctrl + O` | Görsel yükle |
| `Ctrl + S` | İşlenmiş görseli kaydet |
| `Ctrl + Z` | Geri al (Undo) |
| `Ctrl + Y` | Yinele (Redo) |

## 📁 Proje Yapısı

```
ImageProcess/
├── form/                          # Ana proje klasörü
│   ├── Form1.cs                   # Ana form ve iş mantığı
│   ├── Form1.Designer.cs          # Form tasarım kodu
│   ├── Form1.resx                 # Form kaynakları
│   ├── Program.cs                 # Uygulama giriş noktası
│   ├── form.csproj                # Proje dosyası
│   ├── ImageClassification.mbconfig # ML.NET Model Builder yapılandırması
│   ├── bin/                       # Derlenmiş dosyalar
│   │   └── Debug/
│   │       └── net10.0-windows/
│   └── obj/                       # Geçici derleme dosyaları
└── form.slnx                      # Visual Studio çözüm dosyası
```

### Önemli Dosyalar

- **Form1.cs**: Tüm görüntü işleme mantığı, filtreler, undo/redo sistemi ve kullanıcı etkileşimleri burada tanımlanmıştır
- **Form1.Designer.cs**: Kullanıcı arayüzü kontrollerinin (butonlar, picturebox'lar) tanımları
- **form.csproj**: Proje bağımlılıkları ve yapılandırmaları

## 🔧 Geliştirme

### Kod Yapısı

Uygulama, modern C# özelliklerini kullanarak geliştirilmiştir:

- **Nullable Reference Types**: Güvenli null yönetimi
- **Stack-based Undo/Redo**: Performanslı geri alma sistemi
- **Memory Management**: Düzgün kaynak yönetimi (Dispose pattern)
- **Error Handling**: Kapsamlı hata yakalama ve kullanıcı bildirimleri

### Önemli Sınıflar ve Metodlar

#### `Form1` Sınıfı

- `LoadImage(string path)`: Görsel yükleme ve format dönüştürme
- `ApplyAndRender(Func<ImageSharpImage, ImageSharpImage> operation)`: Filtre uygulama ve görüntüleme
- `RenderOriginal()` / `RenderProcessed()`: Görsel render işlemleri
- `AdjustZoom(int delta)`: Zoom kontrolü
- `ImageToPictureBox(...)`: ImageSharp görselini PictureBox'a dönüştürme

### Yeni Özellik Ekleme

Yeni bir filtre eklemek için:

1. `Form1.Designer.cs` dosyasına yeni bir buton ekleyin
2. `Form1.cs` dosyasına buton click event handler'ı ekleyin:
   ```csharp
   private void btnNewFilter_Click(object? sender, EventArgs e)
   {
       ApplyAndRender(img => img.Clone(x => x.YourFilterMethod()));
   }
   ```

### Performans İyileştirmeleri

- Görseller klonlanarak işlenir, orijinal görsel korunur
- Undo/Redo stack'leri bellek kullanımını optimize eder
- Büyük görseller için otomatik yeniden boyutlandırma yapılır

## 🐛 Bilinen Sorunlar

- Çok büyük görseller (10MB+) yüklenirken performans düşebilir
- Undo/Redo stack'i sınırsızdır, çok fazla işlem yapıldığında bellek kullanımı artabilir

## 🔮 Gelecek Özellikler

- ML.NET ile görüntü sınıflandırma
- Daha fazla filtre seçeneği (Brightness, Contrast, Saturation vb.)
- Toplu işleme (birden fazla görseli aynı anda işleme)
- Filtre parametrelerini ayarlama (blur miktarı, keskinleştirme seviyesi vb.)
- Görsel kırpma (crop) özelliği
- Renk düzeltme araçları

## 📝 Notlar

- ML.NET kütüphaneleri projeye eklenmiştir ancak şu an kullanılmamaktadır. Gelecekte görüntü sınıflandırma özellikleri için hazırlanmıştır.
- Uygulama, ImageSharp'ın desteklemediği formatlar için System.Drawing fallback mekanizması kullanır.

## 📄 Lisans

Bu proje eğitim amaçlı geliştirilmiştir. Kullanımınız kendi sorumluluğunuzdadır.

## 👤 Geliştirici

Proje geliştirilmiştir ve sürekli iyileştirilmektedir.

---

**Not**: Bu README dosyası projenin mevcut durumunu yansıtmaktadır. Yeni özellikler eklendikçe güncellenecektir.

