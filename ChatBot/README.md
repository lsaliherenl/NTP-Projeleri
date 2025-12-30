# ChatBot 💬

Modern ve kullanıcı dostu bir arayüze sahip, Google Gemini API destekli C# WinForms sohbet uygulaması.

## ✨ Özellikler

*   **Google Gemini Entegrasyonu:** Güçlü AI modelleriyle akıllı sohbet deneyimi
*   **Modern Arayüz:** Yuvarlatılmış köşeler, şık sohbet baloncukları ve temiz tasarım
*   **Markdown Desteği:** Bot yanıtlarında kalın, italik ve diğer formatlamaları görüntüleme
*   **Sohbet Geçmişi:** Bağlamı koruyan akıllı sohbet hafızası
*   **Güvenli Yapılandırma:** API anahtarları kaynak kodundan ayrı tutulur

## Kurulum ve Kullanım

### Gereksinimler

*   .NET Framework 4.8
*   Visual Studio 2022 veya daha yenisi

### Yapılandırma

Bu proje Google Gemini API kullanmaktadır. Çalıştırmadan önce bir API anahtarı almanız gerekir.

1.  [Google AI Studio](https://aistudio.google.com/) üzerinden bir API anahtarı edinin
2.  Projeyi klonlayın veya indirin:
   ```bash
   git clone https://github.com/YOUR_USERNAME/ChatBot.git
   ```
3.  `ChatBot/ChatBot/config.example.json` dosyasını `ChatBot/ChatBot/config.json` olarak kopyalayın
4.  `config.json` dosyasını bir metin editörüyle açın ve `"YOUR_API_KEY_HERE"` yerine kendi API anahtarınızı yapıştırın

```json
{
  "ApiKey": "BURAYA_API_ANAHTARINIZI_YAZIN"
}
```

**Önemli:** `config.json` dosyasını asla GitHub'a veya herkese açık bir yere yüklemeyin. `.gitignore` dosyası bu dosyanın yanlışlıkla yüklenmesini engeller.

### Çalıştırma

**Visual Studio ile:**
1.  `ChatBot/ChatBot.sln` dosyasını Visual Studio ile açın
2.  Projeyi derleyin ve çalıştırın (F5)

**Komut satırı ile:**
```bash
cd ChatBot
dotnet build ChatBot.sln
dotnet run --project ChatBot/ChatBot.csproj
```

## 📝 Notlar

- `config.json` dosyası `.gitignore` ile korunmaktadır ve GitHub'a yüklenmeyecektir
- API anahtarınızı asla paylaşmayın veya public repository'lere yüklemeyin
- İlk çalıştırmada `config.json` dosyasını oluşturmayı unutmayın

## 🤝 Katkıda Bulunma

Katkılarınızı bekliyoruz! 

1. Bu repository'yi fork edin
2. Yeni bir branch oluşturun (`git checkout -b feature/yeni-ozellik`)
3. Değişikliklerinizi commit edin (`git commit -am 'Yeni özellik eklendi'`)
4. Branch'inizi push edin (`git push origin feature/yeni-ozellik`)
5. Bir Pull Request oluşturun

Hatalar için [Issue](https://github.com/YOUR_USERNAME/ChatBot/issues) açabilirsiniz.

## 📄 Lisans

Bu proje [MIT Lisansı](LICENSE) ile lisanslanmıştır.
