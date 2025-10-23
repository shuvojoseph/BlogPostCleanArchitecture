# BlogApp – Clean Architecture .NET 9 Web API

A sample blog application built with **.NET 9**, following **Clean Architecture** principles.
This project demonstrates how to structure a modern Web API with **Domain-Driven Design**, **Entity Framework Core**, **JWT Authentication**, and **xUnit tests**.

---

## 🚀 Features

* **Clean Architecture Layers**: Domain, Application, Infrastructure, and API (Presentation)
* **Entity Framework Core** with SQL Server
* **JWT Authentication** (Access & Refresh tokens)
* **User Management** (Register, Login, Get All Users)
* **Blog Management** (CRUD operations)
* **xUnit & Moq Testing** (Unit & Integration tests)
* **Docker-friendly** (run DB with SQL Server in a container)

---

## 📂 Project Structure

```
BlogApp/
├── BlogApp.API/             # Presentation Layer (Controllers, Startup)
├── BlogApp.Application/     # Application Layer (Use Cases, DTOs, Services)
├── BlogApp.Domain/          # Domain Layer (Entities, Interfaces)
├── BlogApp.Infrastructure/  # Infrastructure Layer (Persistence, Repositories)
└── BlogApp.Tests/           # Test Projects (Unit & Integration)
```

---

## ⚙️ Getting Started

### 1️⃣ Clone and Build

```bash
git clone https://github.com/shuvojoseph/BlogPostCleanArchitecture.git
cd BlogPostCleanArchitecture
dotnet build
```

### 2️⃣ Setup Database

* On **Mac/Linux** → use Docker:

  ```bash
  docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=JoSePh1234" \
    -p 1433:1433 --name sqlserver \
    -d mcr.microsoft.com/mssql/server:2022-latest
  ```
* On **Windows** → install **SQL Server Express/Developer** locally.

Update the connection string in **`BlogApp.API/appsettings.json`** and **`BlogApp.Infrastructure/appsettings.json`**:

```json
"ConnectionStrings": {
  "DevConnection": "Server=localhost,1433;Database=BlogPosCleantDB;User Id=SA;Password=JoSePh1234;TrustServerCertificate=True;"
}
```

### 3️⃣ Configure JWT

In **`BlogApp.API/appsettings.json`**:

```json
"Jwt": {
  "Key": "YourSuperSecretKeyWithAtLeast32CharactersLong123!",
  "Issuer": "http://localhost:5186",
  "Audience": "http://localhost:5186",
  "ExpiryInMinutes": 60,
  "RefreshTokenExpiryInDays": 7
}
```

### 4️⃣ Run Database Migrations

```bash
cd BlogApp.Infrastructure
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5️⃣ Run the API

```bash
cd ../BlogApp.API
dotnet run
```

API should now be available at 👉 **[http://localhost:5186/swagger](http://localhost:5186/swagger)**

---

## 🧪 Running Tests

```bash
cd BlogApp.Tests
dotnet build
dotnet test
```

Tests use:

* [xUnit](https://xunit.net/)
* [Moq](https://github.com/moq/moq4)
* [Microsoft.AspNetCore.Mvc.Testing](https://learn.microsoft.com/en-us/dotnet/core/testing/integration-testing)

---

## 🛠️ How It Was Built (from scratch)

1. Create solution and projects:

   ```bash
   dotnet new sln -n BlogApp
   dotnet new classlib -n BlogApp.Domain
   dotnet new classlib -n BlogApp.Application
   dotnet new classlib -n BlogApp.Infrastructure
   dotnet new xunit -n BlogApp.Tests
   dotnet new webapi -n BlogApp.API
   ```

2. Add project references:

   * API → Application, Domain, Infrastructure
   * Application → Domain
   * Infrastructure → Application, Domain
   * Tests → All layers

3. Add required NuGet packages (EF Core, Identity, JwtBearer, Swagger, Moq, xUnit).

4. Setup EF Core with **DesignTimeDbContextFactory** in `Infrastructure` for migrations.

5. Implement **Clean Architecture flow**:

   * Domain → Entities & Interfaces
   * Application → Use Cases (Services)
   * Infrastructure → EF Repositories
   * API → Controllers, JWT Auth

---

## 📖 Documentation

* Swagger UI → `http://localhost:5186/swagger`
* Example endpoints:

  * `POST /api/auth/register` → Register new user
  * `POST /api/auth/login` → Authenticate user
  * `GET /api/users` → Get all users except logged in one (requires JWT)
  * `GET /api/blogs` → Get all blogs (requires JWT)

---

## 💡 Notes

* Migration commands **must be run from `Infrastructure`** (not API).
* Tests are split into **Unit** (AuthService, BlogService) and **Integration** (AuthController).
* API depends on Infrastructure (unlike pure Clean Architecture) → done for simplicity.

---

## 🧑‍💻 Author

👤 **Shuvo Joseph**

* GitHub: [@shuvojoseph](https://github.com/shuvojoseph)
* LinkedIn: [Your LinkedIn link]

