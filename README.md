# GlobalStore Solution

This repository contains the `GlobalStore` solution. The application is divided into two main components:

- **GlobalStore.Api**: A RESTful API built with .NET 8 for store management.
- **GlobalStore.Functions**: An Azure Function for managing products (CRUD), connected to SQL Server.
- **PostmanCollection**: Postman requests to verify the functionality of each endpoint.
  
---

## 🧭 Table of Contents

- [📖 Description](#description)
- [🛠️ Technologies Used](#technologies-used)
- [📮 API Endpoints](#api-endpoints)
- [🧪 Testing](#testing)

---

## 📖 Description

The `GlobalStore` system was designed to support a globalized and multi-tenant environment, where multiple companies can manage their respective stores. Product data is handled through Azure Functions connected to a SQL Server database, including stored procedures and scalar functions.

---

## 🛠️ Technologies Used

- **.NET 8**
- **ASP.NET Core Web API**
- **Azure Functions (Isolated Process)**
- **Azure App Service with deployment slots**
- **SQL Server** (Products table, stored procedure, scalar function)
- **GitHub Actions** (CI/CD)
- **xUnit & Moq** (TDD)
- **Swagger (Swashbuckle)** for API documentation
- **Postman** for endpoint validation

---

## 📮 API Endpoints
Examples available via Swagger.
https://globalstore-api-staging.azurewebsites.net/index.html

## 🧪 Testing
Tests were implemented using:

- xUnit
- Moq
- Test project: GlobalStore.Tests

  
