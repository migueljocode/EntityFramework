# EntityFramework — EF-Based Solutions & Samples

> A collection of Entity Framework (EF) examples and sample projects demonstrating clean architecture, data access, and practical usage of EF Core.

---

## 📄 Overview

This repository contains small projects and samples built with **Entity Framework Core**, designed to help you learn, test, and build real-world data-driven applications.

It includes example projects such as:

* **LibraryManagement** — a simple CRUD-based demo showing how to set up entities, DbContext, and database operations.

You can expand this repo with additional EF experiments, prototypes, or reusable templates.

---

## 📂 Repository Structure

```
EntityFramework/
│
├── LibraryManagement/       # Sample project demonstrating EF Core CRUD
│   ├── Models/              # Entities
│   ├── Data/                # DbContext + configuration
│   ├── Services/            # (Optional) business or repository logic
│   ├── Migrations/          # EF migrations folder (if used)
│   └── Program.cs
│
└── .gitignore
```

*(Adjust this section to match your exact structure.)*

---

## 🎯 Why This Repo Is Useful

* Shows **clean EF Core setup** (entities → context → queries → CRUD).
* Demonstrates basic EF techniques: migrations, relationships, LINQ, database creation.
* Serves as a **template** when starting new EF projects.
* Helps you understand and practice EF Core simply and practically.

---

## 🚀 Getting Started

### 1. Clone the repository

```
git clone https://github.com/migueljocode/EntityFramework.git
```

### 2. Open in IDE

Use **Visual Studio**, **Rider**, or **VS Code** (with C# extensions).

### 3. Restore dependencies

```
dotnet restore
```

### 4. Configure your database

Find the connection string in:

* `appsettings.json`, or
* inside `DbContext` class

Update it with your SQL Server / SQLite / PostgreSQL connection.

### 5. Apply migrations (if enabled)

```
dotnet ef database update
```

### 6. Run the sample project

```
dotnet run --project LibraryManagement
```

---

## 🧰 Requirements

* .NET 6 / .NET 7 / .NET 8
* Entity Framework Core
* SQL Server or SQLite (depending on the project)

You can install EF tools using:

```
dotnet tool install --global dotnet-ef
```

---

## 📘 Usage Example (General EF Pattern)

```csharp
using var context = new AppDbContext();

// Create
context.Books.Add(new Book { Title = "Sample", Author = "Author" });
context.SaveChanges();

// Read
var books = context.Books.ToList();

// Update
var book = context.Books.First();
book.Title = "Updated Title";
context.SaveChanges();

// Delete
context.Books.Remove(book);
context.SaveChanges();
```

---

## 🧱 Architecture Notes

This repo can be used to learn or practice:

* Clean Data Layer design
* Repository Pattern *(optional but common)*
* Unit of Work
* LINQ Querying
* EF Relationships (1-1, 1-n, n-n)
* Migrations and schema evolution

---

## 📌 Roadmap (optional — you can edit later)

* Add more sample projects
* Add unit tests
* Add PostgreSQL and SQLite examples
* Add advanced EF demos (tracking vs no-tracking, concurrency, raw SQL)

---

## 👤 Author

**migueljocode**
Feel free to fork, explore, and use these samples in your learning journey.

---

## 📜 License

You can add a license here if you want (MIT recommended).
If no license is added, GitHub treats it as “all rights reserved”.

