# Engas API

A comprehensive ASP.NET Core 9.0 Web API for managing fuel station operations, including store management, staff, customers, inventory, orders, and authentication.

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 9.0
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core 9.0
- **Authentication**: JWT Bearer
- **API Documentation**: Swashbuckle (Swagger)
- **Testing**: Microsoft Playwright

## 📦 NuGet Packages

| Package | Version |
|---------|---------|
| HtmlAgilityPack | 1.11.72 |
| Microsoft.AspNetCore.Authentication.JwtBearer | 9.0.0 |
| Microsoft.AspNetCore.OpenApi | 9.0.9 |
| Microsoft.EntityFrameworkCore | 9.0.0 |
| Microsoft.EntityFrameworkCore.Design | 9.0.0 |
| Microsoft.EntityFrameworkCore.Tools | 9.0.0 |
| Npgsql.EntityFrameworkCore.PostgreSQL | 9.0.1 |
| Microsoft.Playwright | 1.49.0 |
| Swashbuckle.AspNetCore | 6.5.0 |

## 🏗️ Project Structure

```
API/
├── Configurations/          # Configuration classes
│   └── JwtConfig.cs
├── Contract/              # Request/Response DTOs
│   ├── Customer/
│   ├── Item/
│   ├── Order/
│   ├── Staff/
│   ├── Store/
│   ├── StoreStock/
│   └── User/
├── Controllers/          # API Controllers
│   ├── AuthControllers/
│   ├── CustomerControllers/
│   ├── ItemControllers/
│   ├── OrderControllers/
│   ├── StaffControllers/
│   ├── StoreControllers/
│   └── StoreStockControllers/
├── Core/
│   └── Enums/            # Enumerations
│       ├── OrderStatus.cs
│       ├── StockLocation.cs
│       └── UserEnum.cs
├── Data/
│   ├── Config/          # EF Core Configurations
│   │   ├── ItemConfiguration/
│   │   ├── OrderConfiguration/
│   │   ├── OrderItemConfiguration/
│   │   ├── StaffConfiguration/
│   │   ├── StoreConfiguration/
│   │   └── StoreStockConfiguration/
│   ├── Context/
│   │   └── AppDBContext.cs
│   └── Models/           # Entity Models
│       ├── Customer/
│       ├── Item/
│       ├── Order/
│       ├── Staff/
│       ├── Store/
│       └── StoreStock/
├── Migrations/          # EF Core Migrations
├── Properties/
│   └── launchSettings.json
├── Services/            # Business Logic Services
│   ├── AuthServices/
│   ├── CustomerService/
│   ├── ItemServices/
│   ├── OrderServices/
│   ├── StaffServices/
│   ├── StoreServices/
│   └── StoreStockService/
├── API.csproj
├── API.http
├── API.sln
├── Program.cs
├── ServiceRegistrationExtensions.cs
├── appsettings.Development.json
└── appsettings.json
```

## 🚀 Getting Started

### Prerequisites

- .NET 9.0 SDK
- PostgreSQL Database
- Visual Studio Code or Visual Studio 2022

### Installation

1. Clone the repository
2. Update the connection string in `appsettings.json`:



2. Update JWT configuration in `appsettings.json`:

```json
{
  "JwtConfig": {
    "Secret": "your-256-bit-secret-key-here-min-32-chars",
    "ExpiryInMinutes": 60
  }
}
```

3. Apply migrations:

```bash
cd API
dotnet ef database update
```

4. Run the application:

```bash
dotnet run
```

The API will be available at `https://localhost:5001` (or `http://localhost:5000`)

## 📚 API Documentation

When running in Development mode, access the Swagger UI at:

```
https://localhost:5001/swagger
```

## 🔐 Authentication

The API uses JWT Bearer authentication. To access protected endpoints:

1. Register a new user/store
2. Login to get a JWT token
3. Include the token in the Authorization header:

```
Authorization: Bearer <your_jwt_token>
```

## 📋 Available Endpoints

### Auth
- `POST /api/auth/register-store` - Register a new store
- `POST /api/auth/login-store` - Store login
- `POST /api/auth/login-staff` - Staff login
- `POST /api/auth/register-customer` - Customer registration
- `POST /api/auth/login-customer` - Customer login

### Stores
- `GET /api/stores` - Get all stores
- `GET /api/stores/{id}` - Get store by ID
- `PUT /api/stores/{id}` - Update store
- `DELETE /api/stores/{id}` - Delete store

### Staff
- `GET /api/staff` - Get all staff
- `GET /api/staff/{id}` - Get staff by ID
- `POST /api/staff` - Create new staff
- `PUT /api/staff/{id}` - Update staff
- `DELETE /api/staff/{id}` - Delete staff

### Customers
- `GET /api/customers` - Get all customers
- `GET /api/customers/{id}` - Get customer by ID
- `POST /api/customers` - Create new customer
- `PUT /api/customers/{id}` - Update customer
- `DELETE /api/customers/{id}` - Delete customer

### Items
- `GET /api/items` - Get all items
- `GET /api/items/{id}` - Get item by ID
- `POST /api/items` - Create new item
- `PUT /api/items/{id}` - Update item
- `DELETE /api/items/{id}` - Delete item

### Store Stock
- `GET /api/store-stock` - Get all stock
- `GET /api/store-stock/{id}` - Get stock by ID
- `POST /api/store-stock` - Create new stock
- `PUT /api/store-stock/{id}` - Update stock
- `DELETE /api/store-stock/{id}` - Delete stock

### Orders
- `GET /api/orders` - Get all orders
- `GET /api/orders/{id}` - Get order by ID
- `POST /api/orders` - Create new order
- `PUT /api/orders/{id}` - Update order
- `DELETE /api/orders/{id}` - Delete order

## 🏗️ Database Schema

### Entities

#### Store
- Id (int, PK)
- Name (string)
- Phone (string)
- City (string)
- Code (string, unique)
- Password (string, hashed)

#### Staff
- Id (int, PK)
- Name (string)
- Email (string)
- Phone (string)
- Role (enum)
- StoreId (int, FK)
- Password (string, hashed)

#### Customer
- Id (int, PK)
- Name (string)
- Email (string)
- Phone (string)
- Address (string)
- Password (string, hashed)

#### Item
- Id (int, PK)
- Name (string)
- Description (string)
- Price (decimal)
- ImageUrl (string)

#### StoreStock
- Id (int, PK)
- ItemId (int, FK)
- StoreId (int, FK)
- Quantity (int)
- Location (enum)

#### Order
- Id (int, PK)
- StoreId (int, FK)
- CustomerId (int, FK)
- StaffId (int, FK)
- OrderDate (DateTime)
- Status (enum)
- TotalAmount (decimal)

#### OrderItem
- Id (int, PK)
- OrderId (int, FK)
- ItemId (int, FK)
- Quantity (int)
- UnitPrice (decimal)
- SubTotal (decimal)

## ⚙️ Configuration



## 🧪 Testing

The project includes Microsoft Playwright for integration testing. Run tests with:

```bash
dotnet test
```

## 📄 License

This project is licensed under the MIT License.

## 👥 Authors

- Engas Development Team
