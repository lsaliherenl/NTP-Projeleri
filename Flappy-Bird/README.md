# Flappy Bird (WinForms, .NET 8)

Basit ama özenli bir Flappy Bird klonu. Windows Forms üzerinde .NET 8 ile geliştirildi.

## Genel Bakış
Kuşu ekranda boruların arasından geçirerek en yüksek skoru yapmayı amaçlarsınız. Zamanla hız artar, hata yaparsanız oyun biter; yeniden deneyebilir ve en yüksek skorunuzu geliştirebilirsiniz.

## Özellikler
- Akıcı kuş kanat animasyonu (3 frame: down/mid/up)
- Rastgele boru konumları ve sabit boşluk (gap)
- Skor; yalnızca boru çifti geçildiğinde artar
- Zorluk ölçekleme (skor yükseldikçe boru hızı artar)
- Başlangıç ve oyun bitti overlay görselleri
- Yeniden başlatma (R veya Space) akışı
- Ses efektleri (wing, point, hit)
- Kalıcı en yüksek skor kaydı ve gösterimi

## Gereksinimler
- Windows 10/11
- .NET 8 SDK (geliştirme için) veya .NET 8 Desktop Runtime (çalıştırma için)
- Visual Studio 2022 (önerilir) veya `dotnet` CLI

## Kurulum
1. Bu depoyu klonlayın veya ZIP olarak indirin.
2. `Flappy-Bird/Flappy Bird/Flappy Bird.sln` dosyasını Visual Studio ile açın.
3. Gerekli NuGet paketleri otomatik indirilecektir (varsa).

Alternatif (CLI):
1. `cd "Flappy-Bird/Flappy Bird/Flappy Bird"`
2. `dotnet build -c Release`

## Çalıştırma
- Visual Studio: `Start` (F5) ile çalıştırın.
- CLI: `dotnet run -c Release` (proje klasöründe).

## Kontroller
- Space: Başlat / Zıplama
- R: Oyun bittiyse yeniden başlatma ekranına dön

## Oynanış Mekanikleri
- Borular ekran dışına çıktığında sağdan rastgele `Y` konumu ile tekrar doğar; `gap` sabittir.
- Skor, kuş boru çiftini tam geçtiğinde bir kez artar.
- Her 5 puanda bir boru hızı bir miktar artar (üst sınır uygulanır).

## Teknik Notlar
- Zamanlayıcılar:
  - `gameTimer` (~20 ms): Fizik, hareket, çarpışma ve skor güncellemesi
  - `birdAnimTimer` (~100 ms): Kuş animasyon frame değişimi
- Yüksek Skor: `%LocalAppData%/FlappyBirdSEC/highscore.txt` konumuna yazılır.

## Özelleştirme (Kaynak: `Flappy Bird/Flappy Bird/Form1.cs`)
- Boru boşluğu: `pipeGap`
- Başlangıç boru hızı: `basePipeSpeed`
- Boru yeniden doğma X aralığı: `RepositionPipesWithRandomGap()` içindeki `x` hesaplaması

## Dizin Yapısı
- `Flappy Bird/` Windows Forms uygulaması
- `Flappy Bird/Resources/` gömülü görseller (kuş, borular, arka plan, zemin, overlay’ler)
- `flappy-bird-assets-master/` orijinal ses ve sprite varlıkları

## Varlıklar ve Lisans
- Ses: `flappy-bird-assets-master/audio/` (`wing.wav`, `point.wav`, `hit.wav`)
- Görseller: `Flappy Bird/Resources/` altında projeye gömülü
- İlgili telif/lisans notları için `flappy-bird-assets-master/README.md` ve `LICENSE` dosyalarına bakın.

## Sorun Giderme
- “Dosya kullanımda/kitli” uyarısı: Çalışan oyunu kapatıp tekrar derleyin.
- Ses çalmıyor: Windows ses cihazınızı ve WAV dosyalarının erişimini kontrol edin.
- Ekran ölçekleme: Form boyutunu değiştirirseniz overlay konumlarını yeniden ayarlamanız gerekebilir.

## Yol Haritası (Öneriler)
- Farklı zorluk modları (değişken `gap`, boru hız eğrileri)
- Dokunmatik/oyun kolu desteği
- Global skor tablosu (bulut)

## Katkı
Pull request ve issue’lar kabul edilir. Hata raporlarında lütfen yeniden üretim adımlarını ve ekran görüntüsü/log eklerini paylaşın.

## Lisans
Okul projesi için geliştirilmiştir istediğiniz gibi kullanabilirisiniz.
