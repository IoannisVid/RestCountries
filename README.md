# RestCountries

This is an ASP.NET Core Web API application which retrieves information regarding countries and stores them.
The project follows a clean architecture using repository patterns with SQL Server database.

## Features

- `POST /api/rest/second` – Expects an array of numbers and returns the second highest among them
- `GET /api/rest` - Retrieves information regarding countries, stores and returns the name, capital and borders of each country

---

## Prerequisites

- [.NET SDK 8.0 or later](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

---

## Getting Started

### 1. Clone the Repository

```bash
    git clone https://github.com/IoannisVid/RestCountries
```
### 2. Configure the Database

In appsettings.json enter your connectionString
```json
"ConnectionStrings": {
  "connectionString": "Server=YOUR_SERVER_NAME;Database=RestCountries;Trusted_Connection=True;TrustServerCertificate=True;"
```
Apply Migrations to create the database schema
```CLI
dotnet ef database update
```
### 3. Build the Application
```CLI
dotnet build
```
### 4. Run the Application
```CLI
dotnet run
```
The api will be available at
http://localhost:7150
Swagger UI will be available at https://localhost:7150/swagger
