# Flappy Bird (WinForms, .NET 8)

Basit ama özellikli bir Flappy Bird klonu. Windows Forms üzerinde .NET 8 ile geliştirildi.

## Özellikler
- Kuş kanat animasyonu (3 frame: down/mid/up)
- Rastgele boru konumları ve sabit boşluk (gap)
- Skor sadece boru çifti geçildiğinde 1 artar
- Zorluk ölçekleme (skor arttıkça boru hızı da artar)
- Başlangıç ekranı ve oyun bitti overlay görselleri
- Yeniden başlatma akışı (R veya Space)
- Ses efektleri (wing, point, hit)
- En yüksek skor kalıcı olarak kaydedilir ve gösterilir

## Kontroller
- Space: Başlat / Zıplama
- R: Oyun bittiyse yeniden başlatma ekranına dön

## Klasör Yapısı
- `Flappy Bird/` uygulama projesi
- `flappy-bird-assets-master/` ses ve orijinal sprite varlıkları

## Varlıklar
- Görseller: `Flappy Bird/Resources/` altında gömülü kaynaklar
  - `message.png` (başlangıç overlay)
  - `gameover.png` (oyun bitti overlay)
  - Kuş, boru, arka plan, zemin görselleri
- Ses: Çalışma zamanında `flappy-bird-assets-master/audio/*.wav` dosyalarından okunur
  - `wing.wav`, `point.wav`, `hit.wav`

## Teknik Notlar
- Zamanlayıcılar:
  - `gameTimer` (20 ms): Oyun döngüsü (fizik, hareket, çarpışma)
  - `birdAnimTimer` (100 ms): Kuş animasyonu (3 frame döngü)
- Borular: Ekran dışına çıkınca sağdan rastgele `Y` ile tekrar doğar, `gap` sabittir.
- Skor: Kuş boru çiftini geçince tek sefer artar.
- Zorluk: Her 5 puanda boru hızı artar (üst sınır 18).
- Yüksek Skor: `LocalApplicationData/FlappyBirdSEC/highscore.txt` dosyasına yazılır.

## Derleme ve Çalıştırma
- Visual Studio veya `dotnet build` ile derleyin.
- Eğer derleme sırasında `Flappy Bird.exe` başka bir işlem tarafından kilitli uyarısı alırsanız, çalışmakta olan oyunu kapatıp tekrar deneyin.

## Özelleştirme
- Boru boşluğu: `Form1.cs` içindeki `pipeGap` alanı
- Başlangıç hızı: `basePipeSpeed`
- Boru yeniden doğma X aralığı: `RepositionPipesWithRandomGap()` içinde `x` hesaplaması

## Bilinen Sınırlamalar
- Ses oynatma sırf `SoundPlayer` ile yapılır; eşzamanlı çoklu ses miksajı yoktur.
- Overlay konumları piksel bazlı ayarlıdır; form boyutu değişirse yeniden ayarlama gerekebilir.

## Teşekkür
- Ses ve sprite varlıkları `flappy-bird-assets` paketinden alınmıştır.
