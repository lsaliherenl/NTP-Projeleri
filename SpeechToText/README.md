# SpeechFlow 🎤

Modern ve kullanıcı dostu bir Speech-to-Text (Konuşma Metne Dönüştürme) uygulaması. Microsoft Azure Cognitive Services Speech API'sini kullanarak gerçek zamanlı konuşma tanıma özelliği sunar.

## ✨ Özellikler

- 🎯 **Gerçek Zamanlı Konuşma Tanıma**: Konuştuğunuz anda metin olarak görüntüleme
- 🎨 **Modern UI**: Avalonia UI ile geliştirilmiş şık ve kullanıcı dostu arayüz
- 🌍 **Çoklu Dil Desteği**: Azure Speech Services'in desteklediği tüm dilleri kullanabilirsiniz
- ⚡ **Hızlı ve Güvenilir**: Microsoft'un güçlü AI teknolojisi ile yüksek doğruluk
- 🔄 **Sürekli Tanıma**: Uzun konuşmaları kesintisiz olarak metne dönüştürme
- 🎭 **Animasyonlu Mikrofon**: Dinleme sırasında görsel geri bildirim

## 🛠️ Teknolojiler

- **.NET 8.0**: Modern C# geliştirme platformu
- **Avalonia UI**: Cross-platform masaüstü UI framework
- **Microsoft Cognitive Services Speech SDK**: Azure Speech Services entegrasyonu
- **XAML**: Modern UI tasarımı

## 📋 Gereksinimler

- Windows 10/11
- .NET 8.0 SDK veya üzeri
- Microsoft Azure Speech Services hesabı
- Mikrofon erişimi

## 🚀 Kurulum ve Çalıştırma

### 1. Projeyi İndirin
```bash
git clone <repository-url>
cd Speech_to_Text-feat-speech-to-text-app
```

### 2. Azure Speech Services Kurulumu
1. [Azure Portal](https://portal.azure.com)'a gidin
2. "Speech Services" kaynağı oluşturun
3. Key ve Region bilgilerinizi alın

### 3. Ortam Değişkenlerini Ayarlayın
PowerShell'de şu komutları çalıştırın:

```powershell
# SpeechFlow klasörüne gidin
cd SpeechFlow

# Ortam değişkenlerini ayarlayın
$env:SPEECH_KEY = "YOUR_SPEECH_KEY_HERE"
$env:SPEECH_REGION = "YOUR_REGION_HERE"

# Uygulamayı çalıştırın
dotnet run
```

### 4. Alternatif Çalıştırma Yöntemleri

#### Visual Studio ile:
1. `SpeechFlow.csproj` dosyasını Visual Studio'da açın
2. `F5` tuşuna basın

#### Visual Studio Code ile:
1. Proje klasörünü VS Code'da açın
2. Terminal'de `dotnet run` komutunu çalıştırın

## 🎮 Kullanım

1. **Uygulamayı Başlatın**: Ortam değişkenlerini ayarladıktan sonra uygulamayı çalıştırın
2. **Mikrofon Butonuna Tıklayın**: 🎤 simgesine tıklayarak konuşma tanımayı başlatın
3. **Konuşun**: Mikrofonunuzun yakınında net bir şekilde konuşun
4. **Metni Görün**: Konuştuğunuz metinler gerçek zamanlı olarak ekranda görünecek
5. **Durdurun**: Mikrofon butonuna tekrar tıklayarak dinlemeyi durdurun

## ⚙️ Yapılandırma

### Desteklenen Bölgeler
- `eastus` - Doğu ABD
- `westus2` - Batı ABD 2
- `westeurope` - Batı Avrupa
- `germanywestcentral` - Almanya Batı Orta
- `eastasia` - Doğu Asya
- Ve daha fazlası...

### Dil Ayarları
Varsayılan olarak sistem dilinizi kullanır. Özel dil ayarları için kodda `SpeechConfig` bölümünü düzenleyebilirsiniz.

## 🔧 Geliştirme

### Proje Yapısı
```
SpeechFlow/
├── App.axaml              # Ana uygulama yapılandırması
├── App.axaml.cs           # Uygulama başlatma kodu
├── MainWindow.axaml       # Ana pencere UI tasarımı
├── MainWindow.axaml.cs    # Ana pencere mantığı
├── WelcomeWindow.axaml    # Hoş geldin penceresi
├── WelcomeWindow.axaml.cs # Hoş geldin penceresi mantığı
├── Program.cs             # Uygulama giriş noktası
└── SpeechFlow.csproj     # Proje yapılandırması
```

### Bağımlılıkları Yükleme
```bash
dotnet restore
```

### Projeyi Derleme
```bash
dotnet build
```

## 🐛 Sorun Giderme

### Yaygın Hatalar

**"SPEECH_KEY and SPEECH_REGION environment variables are not set"**
- Çözüm: Ortam değişkenlerini doğru şekilde ayarladığınızdan emin olun

**"Couldn't find a project to run"**
- Çözüm: `SpeechFlow` klasörüne gidin: `cd SpeechFlow`

**Mikrofon çalışmıyor**
- Çözüm: Mikrofon izinlerini kontrol edin ve ses seviyesini ayarlayın

**Düşük tanıma doğruluğu**
- Çözüm: Net konuşun, arka plan gürültüsünü azaltın

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

## 🤝 Katkıda Bulunma

1. Bu repository'yi fork edin
2. Feature branch oluşturun (`git checkout -b feature/AmazingFeature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add some AmazingFeature'`)
4. Branch'inizi push edin (`git push origin feature/AmazingFeature`)
5. Pull Request oluşturun

## 📞 Destek

Herhangi bir sorun yaşarsanız:
- GitHub Issues bölümünde sorun bildirin
- Detaylı hata mesajlarını paylaşın
- Sistem bilgilerinizi ekleyin

## 🔮 Gelecek Özellikler

- [ ] Çoklu dil seçimi
- [ ] Ses kaydetme özelliği
- [ ] Metin dışa aktarma (TXT, PDF)
- [ ] Özel komutlar ve kısayollar
- [ ] Tema seçenekleri
- [ ] Ses seviyesi göstergesi

---

**Not**: Bu uygulama Microsoft Azure Speech Services kullanır. Azure hesabınızda Speech Services kaynağı oluşturmanız ve geçerli bir API key'i almanız gerekmektedir .
