# 🎉 BlazorAndAIWithMeetUp

A modern, AI-powered event management web application built with **Blazor Server** and **.NET**, featuring Google Authentication and Azure OpenAI integration.

---

## 📋 Table of Contents

- Features
- Technologies
- Prerequisites
- Installation
- Configuration
- Authentication Setup
- AI Integration
- Application Features
- Project Structure
- Database Migrations
- Security Best Practices


---

## ✨ Features

### 🎯 Core Features
- Event creation and management
- Event discovery by categories
- RSVP system
- Comment and review system
- Organizer rating system

### 🔐 Security & Authentication
- Google OAuth 2.0 authentication
- Role-based authorization (User / Organizer)
- Secure data access policies

### 🤖 AI-Powered Features
- Azure OpenAI ChatBot integration
- Smart event recommendations
- AI-assisted user support

### 📱 Modern UI/UX
- Fully responsive design
- Interactive Blazor Server components
- Modern CSS and UI patterns

---

## 🛠️ Technologies

| Technology | Purpose |
|------|--------|
| **.NET 9.0** | Application Framework |
| **Blazor Server** | Frontend UI |
| **ASP.NET Core** | Backend |
| **SQL Server** | Database |
| **Entity Framework Core** | ORM |
| **Google OAuth 2.0** | Authentication |
| **Azure OpenAI** | AI Services |
| **AutoMapper** | Object Mapping |
| **SixLabors.ImageSharp** | Image Processing |

---

## 📦 Prerequisites

- .NET 9.0 SDK
- SQL Server
- Google OAuth Credentials
- Azure OpenAI API Key
- Git

---

## 🚀 Installation

### 1. Clone the repository

```bash
git clone https://github.com/onuracarsoy/BlazorAndAIWithMeetUp.git
cd BlazorAndAIWithMeetUp
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Configure database

Update `appsettings.json` connection string and run:

```bash
dotnet ef database update
```

### 4. Run the application

```bash
dotnet run
```

Application will run on:
```
https://localhost:5001
```

---

## ⚙️ Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MeetUpDb;Trusted_Connection=True;"
  },
  "Google": {
    "ClientId": "YOUR_GOOGLE_CLIENT_ID",
    "ClientSecret": "YOUR_GOOGLE_CLIENT_SECRET"
  },
  "AzureOpenAI": {
    "ApiKey": "YOUR_AZURE_OPENAI_KEY",
    "Endpoint": "YOUR_ENDPOINT"
  }
}
```

> ⚠️ Never commit secrets to version control.

---

## 🔐 Authentication Setup (Google OAuth)

1. Go to Google Cloud Console
2. Create a project
3. Enable OAuth 2.0
4. Create credentials
5. Set redirect URI:

```
https://localhost:5001/signin-google
```

6. Add ClientId and ClientSecret to `appsettings.json`

---

## 🤖 AI Integration

Features powered by Azure OpenAI:
- Smart chatbot assistant
- Event recommendations
- Automated support responses

---

## 🎨 Application Features

### Event Management
- Create / Edit / Delete events
- Capacity management
- Ticket pricing
- Refund policy
- Online / Physical events

### User Experience
- Google login
- Personal event history
- RSVP tracking
- Organizer profiles

---

## 📁 Project Structure

```
BlazorAndAIWithMeetUp/
├── Data/
│   ├── ApplicationDbContext.cs
│   └── Entities/
├── Features/
│   ├── AIChatBot/
│   ├── CreateEvent/
│   ├── DiscoverEvents/
│   ├── RSVPEvent/
│   └── ...
├── Shared/
│   ├── Components/
│   ├── Services/
│   └── ViewModels/
└── wwwroot/
```

---

## 🗄️ Database Migrations

```bash
# Create migration
dotnet ef migrations add InitialCreate

# Apply migration
dotnet ef database update
```

---

## 🔒 Security Best Practices

- HTTPS enabled
- Secure OAuth flow
- Token-based authentication
- Role-based authorization
- Secure API access
- Environment variables for secrets
- Data isolation per user



**Built with ❤️ using Blazor, .NET, AI and modern web technologies**

