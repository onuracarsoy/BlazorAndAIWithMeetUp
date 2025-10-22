# 🎉 MeetUp Web App

Blazor Server ile geliştirilmiş modern etkinlik yönetim platformu.

## ✨ Özellikler

- 🔐 Google Authentication ile giriş
- 🎯 Etkinlik oluşturma, düzenleme, silme
- 🔍 Etkinlik keşfi ve RSVP sistemi
- 🤖 Azure OpenAI ile AI chatbot
- 💬 Etkinlik yorumları ve organizatör değerlendirme
- 👥 Rol tabanlı yetkilendirme (Kullanıcı/Organizatör)

## 🛠️ Teknolojiler

- **.NET 9.0** & **Blazor Server**
- **Entity Framework Core** & **SQL Server**
- **Azure OpenAI** & **Google Authentication**
- **Vertical Slice Architecture**

## 🚀 Kurulum

1. Projeyi klonlayın
```bash
git clone https://github.com/kullaniciadi/MeetUpWebApp.git
cd MeetUpWebApp
```

2. Paketleri yükleyin ve veritabanını oluşturun
```bash
dotnet restore
dotnet ef database update
```

3. `appsettings.json` dosyasında gerekli konfigürasyonları yapın:
   - Veritabanı bağlantı stringi
   - Google OAuth bilgileri
   - Azure OpenAI API bilgileri

4. Uygulamayı çalıştırın
```bash
dotnet run
```
## 📁 Proje Yapısı

## 🔧 Konfigürasyon

- **Google Auth**: [Google Cloud Console](https://console.cloud.google.com/)
- **Azure AI**: [Azure AI Studio](https://azure.microsoft.com/en-us/products/ai-foundry)

---

⭐ Beğendiyseniz yıldız vermeyi unutmayın!



