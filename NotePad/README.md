# Notepad Clone

Windows Forms tabanlı basit bir metin düzenleyici uygulaması. Windows Notepad'in temel özelliklerini içeren bir klon uygulamadır.

## 📋 Özellikler

### Dosya İşlemleri
- **Yeni Dosya**: Yeni bir metin dosyası oluşturma (Ctrl+N)
- **Dosya Açma**: Mevcut metin dosyalarını açma (Ctrl+O)
- **Kaydet**: Dosyayı kaydetme (Ctrl+S)
- **Farklı Kaydet**: Dosyayı farklı bir konuma kaydetme (Ctrl+Shift+S)
- **Çıkış**: Uygulamadan çıkış

### Düzenleme İşlemleri
- **Kes**: Seçili metni kesme (Ctrl+X)
- **Kopyala**: Seçili metni kopyalama (Ctrl+C)
- **Yapıştır**: Panodan metin yapıştırma (Ctrl+V)
- **Tümünü Seç**: Tüm metni seçme (Ctrl+A)

### Diğer Özellikler
- Kaydedilmemiş değişiklikler için uyarı sistemi
- Çok satırlı metin düzenleme desteği
- Yatay ve dikey kaydırma çubukları
- Dosya adını başlık çubuğunda gösterme
- Hakkında penceresi

## 🛠️ Teknolojiler

- **.NET 8.0**: Hedef framework
- **Windows Forms**: Kullanıcı arayüzü
- **C#**: Programlama dili

## 📦 Gereksinimler

- .NET 8.0 SDK veya üzeri
- Windows işletim sistemi
- Visual Studio 2022 veya Visual Studio Code (önerilen)

## 🚀 Kurulum ve Çalıştırma

### 1. Projeyi Klonlayın
```bash
git clone <repository-url>
cd NotePad
```

### 2. Projeyi Derleyin
```bash
dotnet build
```

### 3. Uygulamayı Çalıştırın
```bash
dotnet run
```

Veya Visual Studio'da projeyi açıp F5 tuşuna basarak çalıştırabilirsiniz.

## 📁 Proje Yapısı

```
NotePad/
├── MainForm.cs              # Ana form mantığı ve olay işleyicileri
├── MainForm.Designer.cs     # Form tasarımı ve UI bileşenleri
├── Program.cs               # Uygulama giriş noktası
├── NotepadClone.csproj      # Proje dosyası
└── README.md                # Bu dosya
```

## ⌨️ Klavye Kısayolları

| İşlem | Kısayol |
|-------|---------|
| Yeni Dosya | `Ctrl+N` |
| Dosya Aç | `Ctrl+O` |
| Kaydet | `Ctrl+S` |
| Farklı Kaydet | `Ctrl+Shift+S` |
| Kes | `Ctrl+X` |
| Kopyala | `Ctrl+C` |
| Yapıştır | `Ctrl+V` |
| Tümünü Seç | `Ctrl+A` |

## 🎯 Kullanım

1. Uygulamayı başlattığınızda boş bir metin düzenleyici penceresi açılır.
2. Metninizi yazabilir veya düzenleyebilirsiniz.
3. Dosya menüsünden yeni dosya oluşturabilir, mevcut dosyaları açabilir veya dosyalarınızı kaydedebilirsiniz.
4. Düzenleme menüsünden metin işlemlerini gerçekleştirebilirsiniz.
5. Kaydedilmemiş değişikliklerle uygulamayı kapatmaya çalıştığınızda, kaydetme seçeneği sunulur.

## 🔧 Geliştirme

Bu proje, Windows Forms kullanarak basit bir metin düzenleyici oluşturmayı gösterir. Proje şu temel kavramları içerir:

- Windows Forms uygulama geliştirme
- Menü çubuğu ve menü öğeleri
- Dosya diyalog pencereleri (OpenFileDialog, SaveFileDialog)
- Olay yönetimi (Event Handling)
- Durum yönetimi (unsaved changes tracking)

## 📝 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

## 👤 Geliştirici

Notepad Clone - Basit Metin Düzenleyici

---

**Not**: Bu uygulama Windows işletim sistemi için tasarlanmıştır ve .NET 8.0 Windows Forms gerektirir.

