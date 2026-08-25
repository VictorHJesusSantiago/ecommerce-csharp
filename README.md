# E-Commerce Platform

A full-featured e-commerce solution built with .NET 8, Clean Architecture, and modern C# practices.

## Architecture

```
src/
├── Ecommerce.Domain        # Domain entities, value objects, events
├── Ecommerce.Application   # DTOs, services, validators, interfaces
├── Ecommerce.Infrastructure # Data access, caching, messaging, external services
├── Ecommerce.Api           # RESTful API with versioning
└── Ecommerce.Web           # Razor Pages MVC frontend
```

## Features

- **Product Catalog**: Categories, brands, variants, images, search
- **Shopping Cart**: Session-based and user-based carts
- **Orders**: Order processing, tracking, history
- **Payment**: Stripe and PayPal integration
- **User Management**: Authentication, profiles, addresses
- **Marketing**: Coupons, promotions, banners, newsletters
- **Reviews**: Product reviews with ratings
- **Inventory**: Warehouse management, stock tracking
- **CMS**: Pages, menus, settings, media
- **Notifications**: Email (SendGrid), SMS, push notifications
- **Caching**: Redis with in-memory fallback
- **Messaging**: RabbitMQ with in-memory fallback
- **API**: Versioned REST API with Swagger
- **Web**: MVC frontend with Razor Pages

## Tech Stack

- .NET 8 / C# 12
- Entity Framework Core 8
- ASP.NET Core 8
- Redis / RabbitMQ
- Stripe / PayPal
- SendGrid / Twilio
- xUnit / Moq / FluentAssertions

## Getting Started

1. Clone the repository
2. Configure connection strings in `appsettings.json`
3. Run `dotnet restore && dotnet build`
4. Run migrations: `dotnet ef database update`
5. Start API: `dotnet run --project src/Ecommerce.Api`
6. Start Web: `dotnet run --project src/Ecommerce.Web`

## Docker

```bash
docker-compose -f docker/docker-compose.yml up --build
```

## API Documentation

Access Swagger UI at: `http://localhost:5000/swagger`

## Testing

```bash
dotnet test
```

## Project Structure

- **Domain**: Entities, Value Objects, Domain Events, Specifications
- **Application**: Business logic, DTOs, Validators, Mappings, Behaviors
- **Infrastructure**: EF Core, Redis, RabbitMQ, Email, SMS, File Storage
- **API**: Controllers, Middleware, Filters, SignalR Hubs
- **Web**: Razor Pages, ViewComponents, Models
- **Tests**: Unit, Integration, Architecture tests
