# University API

## Project Overview
The University API is an ASP.NET Core application that provides CRUD operations for universities, departments, courses, and students. It includes authentication using JWT tokens and supports relational database storage.

## Features
- CRUD operations for:
  - Students
  - Courses
  - Departments
  - Universities
- JWT-based authentication and role-based authorization.
- Persistent database with one-to-many relationships.
- API documentation with Swagger.

## Technology Stack
- ASP.NET Core 6.0
- Entity Framework Core
- MySQL with Pomelo
- JWT Authentication
- Swagger for API Documentation

## Setup and Installation
### Prerequisites
- Install .NET 6 SDK.
- Install MySQL Server.
- Install Postman (optional, for testing).

### Steps
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/sejal-sali/university-api.git
   cd university-api
