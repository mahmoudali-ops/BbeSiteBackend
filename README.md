🌍 BbeSite — Tourism Backend API

A scalable backend API for a tourism platform that manages tours, destinations, transfers, hotels, and bookings with multi-language support and modern backend architecture.

The system is designed to support tourism agencies and travel platforms by providing secure APIs, high performance caching, and clean architecture principles.

Built with ASP.NET Core (.NET 8) and Entity Framework Core, the project follows enterprise-level backend patterns such as Repository, Unit of Work, and Specification Pattern.

🚀 Tech Stack
Layer	Technologies
⚙️ Runtime	.NET 8
🌐 API	ASP.NET Core Web API, Swagger (Swashbuckle)
🗄 Database	SQL Server
📦 ORM	Entity Framework Core 9
🔐 Authentication	ASP.NET Core Identity + JWT
🛡 Authorization	Role-based authorization
🔄 Mapping	AutoMapper
✔ Validation	Data Annotations / FluentValidation
⚡ Caching	Redis (StackExchange.Redis)
📧 Email Service	SMTP (Zoho / other providers)
🖼 Image Processing	SixLabors.ImageSharp (WebP optimization)
🏗 Architecture

The project follows a Clean Layered Architecture separating responsibilities across four main layers:

TourSite.APIs
│
├── Controllers
├── Middleware
└── Configuration

TourSite.Service
│
├── Business Logic
├── DTOs
└── Application Services

TourSite.Repository
│
├── Repositories
├── Unit Of Work
└── EF DbContext

TourSite.Core
│
├── Domain Entities
├── Interfaces
└── Specifications
Design Patterns Used

📦 Repository Pattern — abstraction over data access

🔄 Unit of Work — transaction management

🔍 Specification Pattern — reusable query logic

🔁 DTO Mapping — AutoMapper for API ↔ Domain

⚙ Middleware Pattern — centralized error handling

✨ Core Features
🧭 Tours Management

Create, update, delete tours

Category management

Image upload and optimization

Pagination and filtering

🌍 Destinations

Multi-destination support

Destination content management

🚐 Transfers

Manage transfer services

Transfer booking support

🏨 Hotels

Hotel data and related bookings

📅 Booking System

Reservation workflow

Booking confirmation

Email notifications

🌐 Multi-Language Support

The platform supports content translations, allowing tours, destinations, and other content to be displayed in multiple languages.

This enables the system to serve international tourism platforms.

🔐 Security

The API implements modern security practices:

🔒 JWT Authentication

🍪 HttpOnly Cookies for token storage (reduces XSS risks)

🛡 Role-based Authorization

🌐 CORS protection

✔ Input validation

⚠ Centralized exception middleware

⚡ Performance Features

Redis caching for frequently accessed endpoints

Custom cache attributes to easily cache API responses

Optimized images using WebP format

Efficient database queries using Specification Pattern

🖼 Image Processing

Images uploaded to the platform are automatically:

Resized

Optimized

Converted to WebP

Using:

SixLabors.ImageSharp

This significantly reduces bandwidth and improves loading performance.

📧 Email Notifications

The system supports sending emails such as:

Booking confirmations

Notifications

System messages

Implemented using SMTP providers (e.g. Zoho).

🛠 Getting Started
Prerequisites

.NET 8 SDK

SQL Server

Redis (optional but recommended)

1️⃣ Clone the Repository
git clone <repository-url>
cd BbeSite
2️⃣ Restore & Build
dotnet restore
dotnet build
3️⃣ Configure Application

Update configuration files:

TourSite.APIs/appsettings.json
TourSite.APIs/appsettings.Development.json

Required settings:

SQL Server connection string

JWT configuration

Redis connection (optional)

SMTP settings (optional)

Example:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=BbeSite;Trusted_Connection=True;"
}
4️⃣ Apply Database Migrations
cd TourSite.APIs
dotnet ef database update

If EF tools are not installed:

dotnet tool install --global dotnet-ef
5️⃣ Run the API
dotnet run --project TourSite.APIs

Swagger UI:

https://localhost:<port>/swagger
📁 Solution Structure
BbeSite
│
├── TourSite.APIs
│   ├── Controllers
│   ├── Middleware
│   └── Configuration
│
├── TourSite.Service
│   ├── DTOs
│   ├── Services
│   └── Business Logic
│
├── TourSite.Repository
│   ├── Repositories
│   ├── UnitOfWork
│   └── DbContext
│
├── TourSite.Core
│   ├── Entities
│   ├── Interfaces
│   └── Specifications
│
