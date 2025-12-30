# 🛡️ Basit Antivirus Tarayıcı

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-11.0-239120?logo=c-sharp)](https://docs.microsoft.com/dotnet/csharp/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Eğitim amaçlı geliştirilmiş, hash tabanlı dosya tarama sistemi. Modern antivirüs yazılımlarının temel çalışma prensibini anlamak için tasarlanmış açık kaynak bir projedir.

## 📖 Proje Hakkında

**Basit Antivirus Tarayıcı**, siber güvenlik alanındaki temel kavramları öğrenmek isteyen geliştiriciler ve öğrenciler için tasarlanmış eğitim amaçlı bir projedir. Bu proje, modern antivirüs yazılımlarının kullandığı **imza tabanlı tespit (signature-based detection)** metodunun basitleştirilmiş bir versiyonunu sunar.

### Projenin Amacı

Bu proje, aşağıdaki konuları öğrenmek ve anlamak için geliştirilmiştir:

- 🔐 **Hash Fonksiyonları**: MD5 algoritması ile dosya parmak izi oluşturma
- 🗄️ **Veritabanı Tabanlı Tespit**: Bilinen zararlı yazılım imzalarının saklanması ve karşılaştırılması
- 🔍 **Dosya Analizi**: Dosya içeriğinin matematiksel özetini çıkarma
- ⚡ **Performans Optimizasyonu**: Dictionary veri yapısı ile hızlı arama algoritmaları

### Neden Bu Proje?

Modern antivirüs yazılımları, milyonlarca zararlı yazılım imzasını içeren devasa veritabanları kullanır. Bu proje, bu karmaşık sistemin temel mantığını basit ve anlaşılır bir şekilde gösterir. Kod yapısı sade ve açıklayıcıdır, böylece her seviyedeki geliştirici projeyi inceleyerek siber güvenlik kavramlarını öğrenebilir.

### Kullanım Senaryoları

- 🎓 **Eğitim**: Siber güvenlik derslerinde hash tabanlı tespit metodunu öğretmek
- 🔬 **Araştırma**: Antivirüs teknolojilerinin çalışma prensiplerini anlamak
- 💻 **Öğrenme**: C# ve .NET ile dosya işleme ve kriptografi kavramlarını öğrenmek
- 🧪 **Test**: EICAR test dosyası ile antivirüs sistemlerinin temel işleyişini test etmek

### Teknik Özellikler

Proje, .NET 10.0 platformu üzerinde C# ile geliştirilmiştir ve şu teknolojileri kullanır:

- **System.Security.Cryptography**: MD5 hash hesaplama için
- **System.IO**: Dosya okuma ve işleme için
- **System.Collections.Generic**: Dictionary veri yapısı ile hızlı arama için

### Güvenlik Notu

⚠️ **Önemli**: Bu proje sadece eğitim amaçlıdır ve gerçek bir güvenlik çözümü değildir. Üretim ortamlarında kullanılmamalıdır. Gerçek güvenlik ihtiyaçlarınız için profesyonel antivirüs yazılımları kullanmanız önerilir.

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [Nasıl Çalışır?](#-nasıl-çalışır)
- [Kurulum](#-kurulum)
- [Kullanım](#-kullanım)
- [Proje Yapısı](#-proje-yapısı)
- [Katkıda Bulunma](#-katkıda-bulunma)
- [Lisans](#-lisans)
- [Uyarı](#-uyarı)

## ✨ Özellikler

- 🔍 **MD5 Hash Analizi**: Dosyaların benzersiz parmak izlerini oluşturur
- ⚡ **Hızlı Tarama**: Milisaniyeler içinde sonuç verir
- 🗄️ **Veritabanı Tabanlı Tespit**: Bilinen zararlı yazılım imzalarını içerir
- 🧪 **EICAR Desteği**: Standart antivirüs test dosyasını tanır
- 💻 **Konsol Arayüzü**: Basit ve kullanıcı dostu komut satırı arayüzü

## 🔧 Nasıl Çalışır?

Bu proje, **imza tabanlı tespit (signature-based detection)** metodunu kullanır:

1. **Hash Hesaplama**: Seçilen dosyanın MD5 hash değeri hesaplanır
2. **Veritabanı Karşılaştırma**: Hash değeri, bilinen zararlı yazılım veritabanıyla karşılaştırılır
3. **Sonuç Raporlama**: Eşleşme durumuna göre kullanıcıya bilgi verilir

### Teknik Detaylar

- **Algoritma**: MD5 (Message Digest Algorithm 5)
- **Veri Yapısı**: Dictionary (Key-Value) tabanlı hızlı arama
- **Platform**: .NET 10.0

## 🚀 Kurulum

### Gereksinimler

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) veya üzeri
- Windows, Linux veya macOS işletim sistemi
- Visual Studio 2022 veya Visual Studio Code (opsiyonel)

### Adımlar

1. **Projeyi klonlayın:**
```bash
git clone https://github.com/kullaniciadi/BasitAntivirus.git
cd BasitAntivirus
```

2. **Projeyi derleyin:**
```bash
dotnet build BasitAntivirus/BasitAntivirus.csproj
```

3. **Çalıştırın:**
```bash
dotnet run --project BasitAntivirus/BasitAntivirus.csproj
```

## 💡 Kullanım

Program başlatıldığında, taramak istediğiniz dosyanın tam yolunu girin:

```
Taranacak dosyanın tam yolunu yapıştır (Çıkış için 'exit'): C:\Users\Kullanici\Desktop\ornek.exe
```

### Örnek Senaryolar

**Temiz Dosya:**
```
Dosya Hash Değeri (MD5): a1b2c3d4e5f6...
[+] Temiz. Bu dosya veritabanımızdaki virüslerle eşleşmedi.
```

**Tehdit Tespiti:**
```
Dosya Hash Değeri (MD5): 44d88612fea8a8f36de82e1278abb02f
[!!!] TEHDİT TESPİT EDİLDİ [!!!]
Tespit Edilen Zararlı: EICAR Test Dosyası (Zararsız)
```

## 📁 Proje Yapısı

```
BasitAntivirus/
├── BasitAntivirus/
│   ├── Program.cs              # Ana uygulama kodu
│   └── BasitAntivirus.csproj   # Proje yapılandırması
├── .gitignore                  # Git ignore kuralları
└── README.md                   # Bu dosya
```

### Kod Yapısı

- `Main()`: Program giriş noktası ve kullanıcı etkileşimi
- `DosyayiTara()`: Dosya tarama mantığı
- `MD5Hesapla()`: Hash hesaplama fonksiyonu
- `VirusVeritabani`: Zararlı yazılım imzaları sözlüğü

## 🤝 Katkıda Bulunma

Katkılarınızı bekliyoruz! Lütfen şu adımları izleyin:

1. Bu repository'yi fork edin
2. Yeni bir branch oluşturun (`git checkout -b feature/yeni-ozellik`)
3. Değişikliklerinizi commit edin (`git commit -m 'Yeni özellik eklendi'`)
4. Branch'inizi push edin (`git push origin feature/yeni-ozellik`)
5. Bir Pull Request oluşturun

### Önerilen İyileştirmeler

- [ ] SHA-256 hash desteği
- [ ] Çoklu dosya tarama
- [ ] JSON tabanlı veritabanı
- [ ] Loglama sistemi
- [ ] GUI arayüzü
- [ ] Gerçek zamanlı izleme

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

## ⚠️ Uyarı

**Bu yazılım sadece eğitim ve öğrenme amaçlıdır.**

- ❌ Gerçek zamanlı koruma sağlamaz
- ❌ Tüm zararlı yazılımları tespit edemez
- ❌ Üretim ortamında kullanılmamalıdır
- ✅ Sadece öğrenme ve araştırma için tasarlanmıştır

Gerçek güvenlik ihtiyaçlarınız için lisanslı ve güncel bir antivirüs yazılımı kullanmanız önerilir.

---

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!
