# User Management API

Minimal .NET 8 Web API demo built with Clean Architecture for user management CRUD operations.

## Overview

This project demonstrates a simple User Management System with the following scope:

- REST API for CRUD operations
- Clean Architecture with 4 layers
- JWT Authentication (Register + Login)
- MediatR for CQRS (Commands and Queries)
- LINQ for querying and ordering user data
- FluentValidation for request validation
- FastEndpoints support alongside existing MVC controllers
- Polly resilience strategies for transient fault handling
- Entity Framework Core for persistence
- Swagger for API documentation (Bearer auth supported)
- Separate Design Patterns console examples for learning core OOP patterns

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- LINQ
- MediatR
- JWT Bearer Authentication
- BCrypt password hashing
- SQL Server provider for EF Core
- FluentValidation
- FastEndpoints
- Polly
- Swagger

## Solution Structure

```text
src/
  UserManagement.API/
  UserManagement.Application/
  UserManagement.Domain/
  UserManagement.Infrastructure/
  DesignPatterns/
UserManagement.slnx
```

## Design Patterns Examples

The solution includes a dedicated console app at `src/DesignPatterns` with basic examples.

### Included Patterns

- Simple Factory
- Factory Method
- Abstract Factory
- Builder
- Prototype
- Singleton
- Repository
- Chain of Responsibility
- Mediator

### Design Patterns Structure

```text
src/DesignPatterns/
  Creational/
    SimpleFactory/
    FactoryMethod/
    AbstractFactory/
    Builder/
    Prototype/
    Singleton/
  Architectural/
    Repository/
  Behavioral/
    ChainOfResponsibility/
    Mediator/
  Program.cs
```

### Run Design Patterns Demo

```powershell
dotnet run --project src/DesignPatterns/DesignPatterns.csproj
```

## Clean Architecture Layers

### Domain

Contains the core business entities:

- User
- AuthCredential

### Application

Contains:

- DTOs
- Repository interfaces
- FluentValidation validators
- MediatR Commands and Handlers
- MediatR Queries and Handlers
- Register / Login Commands and Handlers

### Infrastructure

Contains:

- EF Core DbContext
- Repository implementation
- Database configuration
- Polly resilience pipelines for database read and lookup operations

### API

Contains:

- Controllers
- FastEndpoints-based endpoint classes
- Swagger configuration
- Dependency injection bootstrap

## Features

- Create user
- Get all users
- Get user by id
- Update user
- Delete user
- Register auth user
- Login and receive JWT token
- Parallel FastEndpoints auth and user endpoints

## Validation Rules

### User DTOs

- FirstName: required, 2 to 50 characters
- LastName: required, 2 to 50 characters
- Email: required, valid email format

### Auth DTOs

Register (POST /api/auth/register)

- Username: required, 3 to 50 characters
- Username: letters, digits, and underscore only
- Password: required, 8 to 100 characters
- Password: must include at least one uppercase letter
- Password: must include at least one lowercase letter
- Password: must include at least one digit
- Password: must include at least one special character

Login (POST /api/auth/login)

- Username: required, max 50 characters
- Password: required, max 100 characters

The same FluentValidation rules are also applied to the FastEndpoints auth and user endpoints.

## Auth Endpoints

| Method | Endpoint | Auth Required | Description |
|--------|----------|---------------|-------------|
| POST | /api/auth/register | No | Register a new auth user |
| POST | /api/auth/login | No | Login and receive JWT token |

## FastEndpoints Auth Endpoints

| Method | Endpoint | Auth Required | Description |
|--------|----------|---------------|-------------|
| POST | /fe/auth/register | No | Register a new auth user using FastEndpoints |
| POST | /fe/auth/login | No | Login and receive JWT token using FastEndpoints |

## User Endpoints (Protected)

> All user endpoints require a valid JWT token in the `Authorization: Bearer {token}` header.

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/users | Create a user |
| GET | /api/users | Get all users |
| GET | /api/users/{id} | Get user by id |
| PUT | /api/users/{id} | Update user |
| DELETE | /api/users/{id} | Delete user |

## FastEndpoints User Endpoints (Protected)

> FastEndpoints user routes also use JWT authentication.

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /fe/users | Create a user |
| GET | /fe/users | Get all users |
| GET | /fe/users/{id} | Get user by id |
| PUT | /fe/users/{id} | Update user |
| DELETE | /fe/users/{id} | Delete user |

## FastEndpoints Notes

- Existing controller endpoints were kept unchanged.
- FastEndpoints were added in parallel for learning and comparison purposes.
- Swagger now shows both controller routes and FastEndpoints routes.
- FastEndpoints validation reuses the same FluentValidation rules as the normal API flow.

## Database Configuration

The current project is configured to use SQL Server because the provided connection string is SQL Server format.

Transient SQL failures are handled in two layers:

- EF Core SQL Server execution strategy retries connection-level transient failures
- Polly resilience pipeline adds retry, circuit breaker, and timeout strategies to repository read and lookup operations

Set the connection string in:

- `src/UserManagement.API/appsettings.json` for a shared default
- `src/UserManagement.API/appsettings.Development.json` for local development overrides

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=DotNet_Order_Management;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

## Running the Project

### Prerequisites

- .NET 8 SDK
- SQL Server instance accessible from your machine

### Restore

```powershell
dotnet restore UserManagement.slnx
```

### Build

```powershell
dotnet build UserManagement.slnx
```

### Run

```powershell
dotnet run --project src/UserManagement.API/UserManagement.API.csproj
```

## Unit Testing

### Test Project

The solution includes a comprehensive xUnit test project (`UserManagement.Tests`) with 33 unit tests covering:

| Category | Test Count | Coverage |
|----------|-----------|----------|
| Validators | 10 | Create/Update User DTOs, Register/Login DTOs |
| Repositories | 14 | User and Auth repository CRUD operations |
| Handlers | 8 | MediatR command handlers for user and auth |
| Resilience | 2 | Polly retry and success scenarios |
| **Total** | **33** | **Core business logic** |

### Running Tests

```powershell
# Run all tests
dotnet test src/UserManagement.Tests/UserManagement.Tests.csproj

```

### Test Organization

```
src/UserManagement.Tests/
  Validators/
    CreateUserValidatorTests.cs         (6 tests)
    RegisterDtoValidatorTests.cs        (4 tests)
  Repositories/
    UserRepositoryTests.cs             (7 tests)
    AuthRepositoryTests.cs             (5 tests)
  Handlers/
    CreateUserCommandHandlerTests.cs    (2 tests)
    LoginCommandHandlerTests.cs         (3 tests)
  Resilience/
    ResiliencePipelineTests.cs          (2 tests)
```

### Test Isolation

- All tests use **in-memory databases** — no data persists to SQL Server
- Each test runs in isolation with its own temporary database instance
- Tests are completely safe to run repeatedly without affecting production data

## Using JWT Auth in Swagger

1. Call `POST /api/auth/register` with a username and password
2. Call `POST /api/auth/login` — copy the `token` from the response
3. Click **Authorize** in Swagger UI, enter `Bearer {token}`
4. All user endpoints are now accessible

### FastEndpoints Auth Flow

If you are testing FastEndpoints routes, register first and then login using the exact same credentials:

```bash
curl -X POST "http://localhost:5210/fe/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "Fe1",
    "password": "Admin@123"
  }'

curl -X POST "http://localhost:5210/fe/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "Fe1",
    "password": "Admin@123"
  }'
```

## Swagger

After running the API, open Swagger UI at:

```text
https://localhost:<port>/swagger
or
http://localhost:<port>/swagger
```

Swagger includes both:

- MVC controller endpoints under `/api/...`
- FastEndpoints routes under `/fe/...`
