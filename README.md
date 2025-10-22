# 🎉 MeetUp Web App

Modern ve kullanıcı dostu bir etkinlik yönetim platformu. Blazor Server ile geliştirilmiş, Google Authentication entegrasyonlu ve AI destekli bir web uygulaması.

## ✨ Özellikler

### 🎯 Temel Özellikler
- **Etkinlik Oluşturma**: Organizatörler kolayca etkinlik oluşturabilir
- **Etkinlik Keşfetme**: Kategorilere göre etkinlikleri keşfedin
- **RSVP Sistemi**: Etkinliklere katılım bildirimi
- **Yorum Sistemi**: Etkinlikler hakkında görüşlerinizi paylaşın
- **Değerlendirme Sistemi**: Organizatörleri değerlendirin

### 🔐 Güvenlik ve Kimlik Doğrulama
- **Google Authentication**: Tek tıkla giriş yapın
- **Rol Tabanlı Yetkilendirme**: Organizatör ve kullanıcı rolleri
- **Güvenli Veri Erişimi**: Kullanıcılar sadece kendi verilerine erişebilir

### 🤖 AI Destekli Özellikler
- **AI ChatBot**: Azure OpenAI entegrasyonu ile akıllı asistan
- **Akıllı Öneriler**: Etkinlik önerileri ve yardım

### 📱 Modern UI/UX
- **Responsive Tasarım**: Tüm cihazlarda mükemmel görünüm
- **Interactive Components**: Blazor Server ile dinamik kullanıcı deneyimi
- **Modern CSS**: Güncel tasarım trendleri

## 🛠️ Teknoloji Stack

- **Frontend**: Blazor Server (.NET 9.0)
- **Backend**: ASP.NET Core Web API
- **Veritabanı**: SQL Server + Entity Framework Core
- **Kimlik Doğrulama**: Google OAuth 2.0
- **AI**: Azure OpenAI
- **Mapping**: AutoMapper
- **Image Processing**: SixLabors.ImageSharp

## 🚀 Kurulum

### Gereksinimler
- .NET 9.0 SDK
- SQL Server
- Google OAuth Credentials
- Azure OpenAI API Key

### Adımlar

1. **Projeyi klonlayın**
   ```bash
   git clone https://github.com/yourusername/MeetUpWebApp.git
   cd MeetUpWebApp
   ```

2. **Bağımlılıkları yükleyin**
   ```bash
   dotnet restore
   ```

3. **Veritabanını yapılandırın**
   - `appsettings.json` dosyasında connection string'i güncelleyin
   - Migration'ları çalıştırın:
   ```bash
   dotnet ef database update
   ```

4. **Google OAuth ayarlarını yapılandırın**
   - `appsettings.json` dosyasında Google ClientId ve ClientSecret'i ekleyin
   ```json
   {
     "Google": {
       "ClientId": "your-client-id",
       "ClientSecret": "your-client-secret"
     }
   }
   ```

5. **Azure OpenAI ayarlarını yapılandırın**
   - Azure OpenAI API anahtarınızı ekleyin

6. **Uygulamayı çalıştırın**
   ```bash
   dotnet run
   ```

## 📁 Proje Yapısı

```
MeetUpWebApp/
├── Data/                    # Veritabanı ve Entity modelleri
│   ├── ApplicationDbContext.cs
│   └── Entities/
├── Features/                # Özellik bazlı modüler yapı
│   ├── AIChatBot/          # AI ChatBot özelliği
│   ├── CreateEvent/        # Etkinlik oluşturma
│   ├── DiscoverEvents/    # Etkinlik keşfetme
│   ├── RSVPEvent/         # RSVP işlemleri
│   └── ...
├── Shared/                 # Paylaşılan bileşenler ve servisler
│   ├── Components/        # Yeniden kullanılabilir bileşenler
│   ├── Services/          # Paylaşılan servisler
│   └── ViewModels/        # Veri transfer nesneleri
└── wwwroot/               # Statik dosyalar
```

## 🎨 Özellik Detayları

### Etkinlik Yönetimi
- Etkinlik oluşturma, düzenleme ve silme
- Kategori bazlı filtreleme (Yüz yüze/Online)
- Kapasite yönetimi
- Bilet fiyatlandırması
- İade politikası

### Kullanıcı Deneyimi
- Google ile tek tıkla giriş
- Kişisel etkinlik geçmişi
- RSVP yönetimi
- Organizatör değerlendirmeleri

### AI Entegrasyonu
- Akıllı chatbot asistanı
- Etkinlik önerileri
- Kullanıcı desteği

### Veritabanı Değişiklikleri
```bash
# Yeni migration oluşturma
dotnet ef migrations add MigrationName

# Migration'ı veritabanına uygulama
dotnet ef database update
```
## 📞 İletişim

Proje hakkında sorularınız için issue açabilir veya iletişime geçebilirsiniz.

---

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!
