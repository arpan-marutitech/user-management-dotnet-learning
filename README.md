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
- Polly resilience strategies for transient fault handling
- Entity Framework Core for persistence
- Swagger for API documentation (Bearer auth supported)

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
- Polly
- Swagger

## Solution Structure

```text
src/
  UserManagement.API/
  UserManagement.Application/
  UserManagement.Domain/
  UserManagement.Infrastructure/
UserManagement.slnx
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

## Auth Endpoints

| Method | Endpoint | Auth Required | Description |
|--------|----------|---------------|-------------|
| POST | /api/auth/register | No | Register a new auth user |
| POST | /api/auth/login | No | Login and receive JWT token |

## User Endpoints (Protected)

> All user endpoints require a valid JWT token in the `Authorization: Bearer {token}` header.

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/users | Create a user |
| GET | /api/users | Get all users |
| GET | /api/users/{id} | Get user by id |
| PUT | /api/users/{id} | Update user |
| DELETE | /api/users/{id} | Delete user |

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

## Using JWT Auth in Swagger

1. Call `POST /api/auth/register` with a username and password
2. Call `POST /api/auth/login` — copy the `token` from the response
3. Click **Authorize** in Swagger UI, enter `Bearer {token}`
4. All user endpoints are now accessible

## Swagger

After running the API, open Swagger UI at:

```text
https://localhost:<port>/swagger
or
http://localhost:<port>/swagger
```
