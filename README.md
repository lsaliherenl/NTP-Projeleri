## NTP-Projeleri

Nesne Tabanlı Programlama dersi kapsamında geliştirdiğim haftalık proje uygulamalarını bu depoda bulabilirsiniz. Aşağıda her proje için kısa bir tanıtım, temel özellikler ve hızlı başlama adımları yer alır.

---

### 1) ChatApp (WPF + .NET 8)
- **Konu**: Çok kullanıcılı sohbet sistemi (TCP tabanlı sunucu + WPF istemci)
- **Öne Çıkanlar**: Satır-bazlı hafif protokol, kullanıcı listesi, avatar senkronizasyonu, modern koyu tema, okunmamış mesaj sayacı, otomatik yeniden bağlanma
- **Klasör**: `Chat_App/`
- **Çalıştırma**:
  - Sunucu:
    - `cd Chat_App/ChatServer/ChatServer`
    - `dotnet run`
  - İstemci (ayrı terminal):
    - `cd Chat_App/ChatClientWPF`
    - `dotnet run`
- **Detaylı dokümantasyon**: `Chat_App/README.md`

---

### 2) Flappy Bird (WinForms, .NET 8)
- **Konu**: Klasik Flappy Bird oyununun Windows Forms ile geliştirilmiş klonu
- **Öne Çıkanlar**: 3 karelilik kuş animasyonu, skor ve zorluk ölçekleme, overlay ekranlar, ses efektleri, kalıcı en yüksek skor
- **Klasör**: `Flappy-Bird/Flappy Bird/`
- **Çalıştırma**:
  - Visual Studio ile `Flappy-Bird/Flappy Bird/Flappy Bird.sln` açıp F5
  - veya proje klasöründen `dotnet build`/`dotnet run`
- **Detaylı dokümantasyon**: `Flappy-Bird/Flappy Bird/README.md`

---

### 3) Safe Type Recorder (Safe Key Logger)
- **Konu**: Yalnızca uygulama odaklı, açık rızaya dayalı güvenli yazı kaydedici
- **Öne Çıkanlar**: Şifre alanlarını atlama, DPAPI ile yerel şifreleme, log rotasyonu, e‑posta ile gönderim, tema ve font ayarları
- **Klasör**: `Safe_Key_Logger/KeyLogger/`
- **Çalıştırma (PowerShell)**:
  - `cd "Safe_Key_Logger/KeyLogger"`
  - `dotnet restore .\KeyLogger\KeyLogger.sln; dotnet build -c Debug .\KeyLogger\KeyLogger.sln`
  - `dotnet run -c Debug --project .\KeyLogger\KeyLogger\KeyLogger.csproj`
- **Detaylı dokümantasyon**: `Safe_Key_Logger/README.md`

---

### 4) SpeechFlow (Speech-to-Text, Avalonia + Azure Speech)
- **Konu**: Microsoft Azure Cognitive Services Speech API ile gerçek zamanlı konuşmayı metne dönüştürme
- **Öne Çıkanlar**: Modern Avalonia UI, çoklu dil, sürekli tanıma, animasyonlu mikrofon
- **Klasör**: `SpeechToText/SpeechFlow/`
- **Ön koşul**: Azure Speech Services anahtarı ve bölgesi (`SPEECH_KEY`, `SPEECH_REGION`)
- **Çalıştırma (PowerShell)**:
  - `cd SpeechToText/SpeechFlow`
  - `$env:SPEECH_KEY = "YOUR_SPEECH_KEY"; $env:SPEECH_REGION = "YOUR_REGION"`
  - `dotnet run`
- **Detaylı dokümantasyon**: `SpeechToText/README.md`

---

### 5) Yüz Tanıma (WinForms + EmguCV)
- **Konu**: Kamera önizleme, yüz algılama, örnek kaydı, LBPH ile eğitim ve canlı tanıma
- **Öne Çıkanlar**: Haar + DNN fallback, 100x100 gri örnekler, model eğitimi ve canlı tanıma, (opsiyonel) sesli geri bildirim
- **Klasör**: `Yuz_Tanima/yuz_tanima/`
- **Çalıştırma**:
  - Visual Studio ile `Yuz_Tanima/yuz_tanima.sln` açın (Configuration: Debug, Platform: x64) ve F5
- **Detaylı dokümantasyon**: `Yuz_Tanima/README.md`

---

### Geliştirme Ortamı
- .NET 8 SDK
- Windows 10/11
- (Proje bazlı ek gereksinimler ilgili README’lerde belirtilmiştir.)

### Lisans ve Notlar
Bu depo eğitim amaçlı projeler içerir. Her projenin kendi lisans/atıf notları ilgili klasör README’sinde yer alır.
