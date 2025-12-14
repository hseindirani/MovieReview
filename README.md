🎬 MovieReview API

A simple, structured ASP.NET Core Web API for managing movies, genres, studios, reviewers, and reviews using the Repository Pattern, DTOs, AutoMapper, 
and Entity Framework Core (PostgreSQL).

🛠 Tech Stack

ASP.NET Core 8 Web API

Entity Framework Core + PostgreSQL

AutoMapper

Repository Pattern

Swagger UI for testing endpoints

Built-in Logging (warnings & errors)

JWT Authentication (Bearer tokens)

📌 Main Features

✔ CRUD for Movies, Genres, Studios, Reviewers, Reviews

✔ Many-to-Many relationships (Movies ↔ Genres, Movies ↔ Studios)

✔ DTOs to prevent exposing full models

✔ AutoMapper for clean object mapping

✔ Error handling & validation

✔ Clean Repository/Interface architecture

✔ Basic server-side logging for debugging and diagnostics

✔ JWT login endpoint and role-based authorization

🧪 Seeding the Database

The project includes a Seed.cs file that populates initial data on startup.

📄 License

This project is for learning purposes and can be freely modified or extended.
