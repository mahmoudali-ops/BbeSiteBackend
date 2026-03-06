🚀 BbeSocial Backend API

Backend API powering the BbeSocial customer service platform, built with ASP.NET Core (.NET 8) using a clean layered architecture focused on scalability, maintainability, and performance.

The API manages the entire platform including services, pricing cards, multilingual content, authentication, and admin operations.

🌍 Live Platform

🔗 https://www.bbesocial.com

🧩 Architecture Overview

The backend follows a Clean Layered Architecture to keep responsibilities separated and maintain a scalable codebase.

API Layer
   ↓
Application Layer
   ↓
Domain Layer
   ↓
Infrastructure Layer
📂 Layers
Layer	Responsibility
API	Controllers, middleware, request handling
Application	Business logic, DTOs, services, validation
Domain	Entities, enums, core models
Infrastructure	EF Core, repositories, database access, external services
🏗 Core Architecture Patterns

The project uses multiple design patterns to ensure clean and maintainable code:

✅ Repository Pattern
Provides abstraction over data access.

✅ Unit of Work Pattern
Ensures transactional consistency across repositories.

✅ Specification Pattern
Handles complex queries in a reusable and clean way.

✅ DTO Pattern
Separates API contracts from domain entities.

✅ AutoMapper
Simplifies mapping between entities and DTOs.

⚙️ Tech Stack
Category	Technologies
Runtime	.NET 8
Framework	ASP.NET Core Web API
ORM	Entity Framework Core 9
Database	SQL Server
Authentication	ASP.NET Core Identity
Caching	Redis
Mapping	AutoMapper
Email	SMTP (Zoho Mail)
API Docs	Swagger / Swashbuckle
🔐 Authentication & Security

Security is implemented using modern best practices.

🔑 Authentication

ASP.NET Core Identity

JWT Authentication

Tokens stored in HttpOnly Secure Cookies

🛡 Authorization

Role-based authorization

Protected admin endpoints

🔒 Additional Security

Input validation

CORS configuration

Secure cookie settings

Global exception handling middleware

🌍 Multi-Language System

The platform supports a dynamic multilingual content system.

Supported languages:

🇬🇧 English
🇩🇪 German
🇳🇱 Dutch
🇸🇦 Arabic (RTL)

Implementation

Content entities have related translation tables.

Example:

Service
 ├── ServiceTranslation
 │      ├── Title
 │      ├── Description
 │      └── LanguageCode

This allows:

✔ dynamic content translation
✔ language switching
✔ dashboard-controlled content

🚀 Performance Optimizations

Several optimizations were implemented to ensure high performance.

⚡ Redis caching for frequently requested data
⚡ Async EF Core queries
⚡ Server-side pagination
⚡ Optimized database queries
⚡ Image compression using WebP

📦 Features
🧩 Services Management

Dynamic service sections

Multi-language content

Admin-controlled visibility

💰 Pricing Cards

Repeatable dynamic pricing cards

Managed fully from dashboard

🖼 Media Management

Upload and manage images

Logo & branding control

🌐 Dynamic Website Content

Pages content controlled by admin

Dynamic translations

📩 Contact System

SMTP email integration (Zoho)

🗄 Database

The database is managed using Entity Framework Core migrations.

Features:

Code-first approach

Database seeding

Migration versioning

Run migrations:

dotnet ef database update
📑 API Documentation

Swagger is enabled for easy testing and documentation.

/swagger

Provides:

✔ endpoint testing
✔ request/response models
✔ authentication support

🧠 Middleware & Infrastructure

Custom middleware implemented for:

✔ Global exception handling
✔ Logging
✔ Request validation
✔ Consistent API responses

🔧 Development Setup
1️⃣ Clone repository
git clone <repo-url>
2️⃣ Configure appsettings

Update:

appsettings.json

Example configuration:

Database connection string

Redis

SMTP settings

JWT configuration

3️⃣ Run migrations
dotnet ef database update
4️⃣ Run the project
dotnet run
📈 Production Ready Features

This API is built with real production deployment in mind:

✔ Clean architecture
✔ Secure authentication
✔ Dynamic multilingual system
✔ Redis caching
✔ Admin-driven content management
✔ Optimized database queries
✔ Exception middleware
✔ Logging support

👨‍💻 Author

Mahmoud Ali

Software Engineer – Backend / .NET Developer
