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
<img width="1607" height="898" alt="image" src="https://github.com/user-attachments/assets/80cf1b5b-7532-4e77-8d6b-620f1651e9d3" />



## 🧪 Testing
Tests were implemented using:

- xUnit
- Moq
- Test project: GlobalStore.Tests

## Azure App Service
<img width="627" height="323" alt="image" src="https://github.com/user-attachments/assets/4ca96817-63be-4e90-a5b2-c0c5594375de" />
<img width="1309" height="174" alt="image" src="https://github.com/user-attachments/assets/a4b9c830-92b0-43e4-a721-af82daf682dc" />

## Azure Function
<img width="568" height="292" alt="image" src="https://github.com/user-attachments/assets/0e7536d6-45a3-4794-9802-c4341a9df0e7" />
<img width="1361" height="459" alt="image" src="https://github.com/user-attachments/assets/36525d9d-3f65-45b3-8e60-cc8ff6d045e8" />

## Azure SQL
<img width="1085" height="660" alt="image" src="https://github.com/user-attachments/assets/b3917b58-a3b9-4e5d-94b5-4a8d0487f075" />

