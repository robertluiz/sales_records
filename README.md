# Ambev Developer Evaluation

Sales management system developed in .NET 8 following Clean Architecture and DDD principles.

[🇧🇷 Versão em Português](README.pt-BR.md)

## 🚀 Technologies

- .NET 8.0
- PostgreSQL 13
- Entity Framework Core
- MediatR
- AutoMapper
- FluentValidation
- BCrypt.NET
- Rebus (Message Bus)
- xUnit
- Docker & Docker Compose

## 📁 Project Structure

```
src/
├── Ambev.DeveloperEvaluation.Application  # Application layer (use cases)
├── Ambev.DeveloperEvaluation.Common       # Shared components
├── Ambev.DeveloperEvaluation.Domain       # Domain layer
├── Ambev.DeveloperEvaluation.IoC          # Dependency injection configuration
├── Ambev.DeveloperEvaluation.ORM          # Persistence layer
├── Ambev.DeveloperEvaluation.Services     # Integration services and message handlers
└── Ambev.DeveloperEvaluation.WebApi       # REST API

tests/
├── Ambev.DeveloperEvaluation.Unit         # Unit tests
├── Ambev.DeveloperEvaluation.Integration  # Integration tests
└── Ambev.DeveloperEvaluation.Functional   # Functional tests
```

## 🏗️ Architecture

The project follows Clean Architecture and Domain-Driven Design (DDD) principles:

- **Domain Layer**: Contains entities, business rules, and repository interfaces
- **Application Layer**: Implements application use cases using CQRS pattern with MediatR
- **Infrastructure Layer**: Repository implementation and data access using Entity Framework Core
- **Services Layer**: Handles integration services and message bus operations with Rebus
- **WebApi Layer**: REST Controllers and API configuration

## 🗃️ Initial Data (Seeds)

### Users

- Admin:
  - Id: 7c9e6679-7425-40de-944b-e07fc1f90ae1
  - Email: admin@ambev.com.br
  - Password: Admin@123
  - Role: Admin
- Customer:
  - Id: 7c9e6679-7425-40de-944b-e07fc1f90ae2
  - Email: customer@email.com
  - Password: Admin@123
  - Role: Customer

### Products

- Brahma Duplo Malte 350ml (Id: 1, Code: BEER-001, $4.99)
- Skol Puro Malte 350ml (Id: 2, Code: BEER-002, $4.49)
- Original 600ml (Id: 3, Code: BEER-003, $8.99)
- Corona Extra 330ml (Id: 4, Code: BEER-004, $7.99)

### Branches

- São Paulo Headquarters (Id: 7c9e6679-7425-40de-944b-e07fc1f90ae7, Code: MATRIX-001)
- Rio de Janeiro Branch (Id: 9c9e6679-7425-40de-944b-e07fc1f90ae8, Code: BRANCH-RJ-001)
- Belo Horizonte Branch (Id: 5c9e6679-7425-40de-944b-e07fc1f90ae9, Code: BRANCH-BH-001)
- Curitiba Branch (Id: 3c9e6679-7425-40de-944b-e07fc1f90ae0, Code: BRANCH-CWB-001)

## 🚦 Business Rules

- Quantity-based discounts:
  - 4-9 items: 10% discount
  - 10-20 items: 20% discount
- Restrictions:
  - Maximum of 20 identical items per sale
  - No discount for less than 4 items

## 🛠️ How to Run

### Prerequisites

- Docker
- Docker Compose
- .NET 8 SDK (for development)

### Using Docker Compose

1. Clone the repository:

```bash
git clone <repository-url>
cd sales_records
```

2. Run the project using Docker Compose:

```bash
docker-compose up -d
```

The API will be available at: http://localhost:8080
Swagger UI: http://localhost:8080/swagger

### Local Development

1. Restore packages:

```bash
dotnet restore Ambev.DeveloperEvaluation.sln
```

2. Run tests:

```bash
dotnet test Ambev.DeveloperEvaluation.sln
```

3. Run the project:

```bash
cd src/Ambev.DeveloperEvaluation.WebApi
dotnet run
```

## 📡 API

A Postman collection is available in the `Ambev.DeveloperEvaluation.postman_collection.json` file with examples of all requests.

### Main Endpoints:

- `POST /api/auth/login` - Authentication
- `GET /api/sales` - List sales
- `POST /api/sales` - Create sale
- `GET /api/sales/{id}` - Sale details
- `PUT /api/sales/{id}/cancel` - Cancel sale
- `PUT /api/sales/{id}/items/{itemId}/cancel` - Cancel sale item

### Documentation

Complete API documentation is available through Swagger UI at:

- Development: http://localhost:8080/swagger
- Production: https://your-domain/swagger

## 🔐 Security

- JWT Authentication
- Passwords encrypted using BCrypt
- HTTPS enabled
- Input validation using FluentValidation

## 📊 Database

- PostgreSQL 13
- Automatic migrations
- Initial data (seeds) for testing
- Optimized indexes for frequent queries

## 🧪 Tests

The project includes:

- 144 unit tests

Run the tests with:

```bash
dotnet test Ambev.DeveloperEvaluation.sln
```

## 📝 License

This project is under the MIT license.
