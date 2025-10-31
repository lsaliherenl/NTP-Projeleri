# ChatApp (WPF + .NET 8)

Modern görünümlü, basit bir çok kullanıcılı sohbet uygulaması. Proje iki kısımdan oluşur:
- ChatServer: TCP tabanlı, satır-bazlı (newline "\n") mesaj protokolünü kullanan .NET 8 sunucu
- ChatClientWPF: WPF (.NET 8, Windows) istemcisi

## İçindekiler
- Özellikler
- Ekran Görüntüsü
- Sistem Gereksinimleri
- Hızlı Başlangıç
- İstemci Kullanımı
- Ayarlar Penceresi
- Mesajlaşma Deneyimi
- Kullanıcı Listesi ve Avatarlar
- Bağlantı Yönetimi
- Okunmamış Mesaj Sayacı
- Klavye Kısayolları
- Tema / UI
- Ağ Protokolü
- Sorun Giderme
- Yol Haritası
- Proje Yapısı
- Lisans

---

## Özellikler
- Çoklu istemci – tek sunucu üstünden tüm istemciler anlık sohbet
- Satır-bazlı hafif TCP protokolü (UTF-8, "\n" ile çerçeveleme)
- Kullanıcı adı ile bağlanma, katılma/ayrılma duyuruları
- Kullanıcı listesi (sol panel) + avatar desteği
- Avatar senkronizasyonu (base64 ile ağ üzerinden yayılım)
- Modern koyu tema, mavi butonlar, mesaj balonları (sağ/sol hizalı)
- Enter ile gönder, Shift+Enter ile yeni satır
- Okunmamış mesaj sayacı (pencere odak dışında artar)
- Bağlantı durumu göstergesi + otomatik yeniden bağlanma (artan bekleme)

## Ekran Görüntüsü
Uygulama mesaj balonları ve koyu tema ile gelir; sol panelde kullanıcılar, orta alanda sohbet, altta mesaj kutusu ve Gönder butonu bulunur.

## Sistem Gereksinimleri
- Windows 10/11
- .NET SDK 8.x

## Hızlı Başlangıç
Sunucuyu ve istemciyi iki ayrı terminalde çalıştırın.

```powershell
# Sunucuyu başlat
cd "ChatServer/ChatServer"
dotnet run

# İstemciyi başlat (ayrı terminal)
cd "ChatClientWPF"
dotnet run
```

Alternatif (solution kökünden):
```powershell
cd "ChatApp"
dotnet run --project .\ChatServer\ChatServer\ChatServer.csproj
# yeni terminal
dotnet run --project .\ChatClientWPF\ChatClientWPF.csproj
```

Öneri: Önce sunucuyu çalıştırın, sonra bir veya birden fazla istemciyi başlatın.

## İstemci Kullanımı
1) Ana pencerede sol üstteki "Ayarlar" butonuna tıklayın.
2) Kullanıcı adınızı, sunucu host ve port bilgisini girin.
3) (İsteğe bağlı) Profil resmi seçin.
4) Kaydet’e tıklayın; istemci sunucuya bağlanır.
5) Mesaj kutusuna yazıp Enter’a basarak gönderin.

Bağlandıktan sonra üst barda durum metni "Bağlı" olarak görünür. Kopma olursa "Yeniden bağlanılıyor…" metniyle artan beklemeli yeniden bağlanma devreye girer.

## Ayarlar Penceresi
- Kullanıcı Adı: Sohbette görünecek ad
- Host/Port: Bağlanılacak sunucu
- Profil Fotoğrafı: Yerel diskinizden bir görsel seçebilirsiniz. Bağlantı kurulduktan sonra avatar ağ üzerinden diğer istemcilere otomatik iletilir.

Not: Avatarlar base64 olarak gönderilir ve diğer istemcilerde bellek içinde saklanır.

## Mesajlaşma Deneyimi
- Balon yapısı: Kendi mesajlarınız sağda, diğerleri solda
- Balon içeriği: `kullanıcı adı: mesaj [HH:mm]`
- Enter: Gönderir, Shift+Enter: yeni satır açar.

## Kullanıcı Listesi ve Avatarlar
- Kullanıcılar solda listelenir.
- Avatarı olan kullanıcıların görselleri yuvarlak bir alanda gösterilir.
- Bir kullanıcı ayrıldığında listeden çıkarılır.

Avatar Senkronizasyonu:
- İstemci bağlanınca `USER:<ad>` sonrası bir kez `AVATAR:<base64>` gönderir (eğer seçilmişse).
- Sunucu avatarı saklayıp herkese `AVATAR:<ad>:<base64>` yayınlar ve yeni bağlananlara bilinen tüm avatarları yollar.

## Bağlantı Yönetimi
- Durum metni: Bağlı / Yeniden bağlanılıyor… / Bağlı değil
- Otomatik yeniden bağlanma: Kopma durumunda artan bekleme (1–5 sn) ile en fazla 10 deneme
- Pencere kapanışında stream’ler ve soketler düzgün kapatılır

## Okunmamış Mesaj Sayacı
- Pencere odak dışındayken gelen mesajlarda başlık `(n) Chat Uygulaması` olarak güncellenir.
- Pencere odak kazanınca sayaç sıfırlanır.

## Klavye Kısayolları
- Enter: Mesajı gönder
- Shift + Enter: Yeni satır

## Tema / UI
- Koyu arka plan (`#0f1115`), açık metin
- Mavi butonlar (`#2563eb` / hover `#1d4ed8`)
- Panel bordürleri: `#2a2f3a`
- Mesaj balonları beyaz, kendi mesajında mavi çerçeve
- Mesaj giriş kutusu çok satırlı, sarmalı

## Ağ Protokolü
UTF-8, newline ("\n") ile çerçevelenmiş metin protokolü:

İstemciden sunucuya
- `USER:<ad>`: İlk mesaj, kullanıcı adı
- `AVATAR:<base64>`: (opsiyonel) Avatar içerik
- `MSG:<metin>`: Sohbet mesajı

Sunucudan istemcilere
- `USERS:<ad1>|<ad2>|...`: O anki kullanıcılar
- `JOIN:<ad>` / `LEAVE:<ad>`: Katılma/ayrılma olayları
- `MSG:<ad>:<metin>`: `<ad>` kullanıcısından mesaj
- `AVATAR:<ad>:<base64>`: `<ad>` kullanıcısının avatar güncellemesi

## Sorun Giderme
- "Bağlanılamadı": Sunucunun çalıştığından, host/port’un doğru olduğundan emin olun. Port 5000 meşgulse server’ı kapatıp tekrar açın ya da farklı port kullanın.
- Mesajlar görünmüyor: Her iki istemcinin de aynı sunucu süreç/farklı port ile uyuştuğunu kontrol edin.
- Avatar görünmüyor: Ayarlar’da resim seçip Kaydet’in ardından yeniden bağlanın. Ağ üzerinden gönderim ilk bağlanmada yapılır.
- VS’de anlamsız uyarılar: Debug Watch/Locals değerlendirmelerini kapatmayı deneyin (Tools → Options → Debugging).

## Yol Haritası
- Tarih ayırıcıları (gün bazlı)
- Kanal/oda yapısı
- JSON tabanlı tam protokol
- Mesaj düzenleme/silme
- Dosya/medya gönderimi
- Sunucu kimlik doğrulama & TLS

## Proje Yapısı
```
ChatApp/
  ChatServer/
    ChatServer/
      Program.cs                # TCP sunucu ve yayın mantığı
  ChatClientWPF/
    MainWindow.xaml             # Ana UI (kullanıcı listesi, sohbet alanı, giriş)
    MainWindow.xaml.cs          # Bağlantı, mesajlaşma, durum yönetimi
    SettingsWindow.xaml(.cs)    # Ayarlar penceresi (ad, host, port, avatar)
    MessageItem.cs              # Mesaj listesi öğesi (Text, IsSelf)
    UserItem.cs                 # Kullanıcı listesi öğesi (Name, Avatar)
```

## Lisans
Bu proje eğitim ve demo amaçlıdır. İhtiyaçlarınıza göre genişletebilirsiniz .
