# Client Management Application API Documentation

## Introduction
The **Client Management Application API** is a robust backend system designed to manage and organize client data for businesses. It enables seamless CRUD operations to create, retrieve, update, and delete client information in a secure and scalable manner.

## Features
- CRUD operations for managing client data
- Authentication and authorization mechanisms
- RESTful API architecture
- Integration with database for persistent storage
- Error handling and logging for improved debugging

## Technologies Used
- **.NET 7** - Framework for building the API
- **Entity Framework Core** - ORM for database interaction
- **SQL Server/PostgreSQL** - Database
- **Swagger** - API documentation and testing
- **JWT** - JSON Web Tokens for authentication
- **Serilog** - Logging framework

## Getting Started

### Prerequisites
To get started with this project, you will need the following:
- .NET SDK (version 7 or higher)
- SQL Server/PostgreSQL for database management
- Git for version control

### Installation
1. Clone the repository:
    ```bash
    git clone https://github.com/DaniElBenson001/ClientMgmtAppAPI.git
    ```
2. Navigate into the project folder:
    ```bash
    cd ClientMgmtAppAPI
    ```
3. Restore the project dependencies:
    ```bash
    dotnet restore
    ```
4. Set up the database connection in `appsettings.json`.
5. Apply database migrations:
    ```bash
    dotnet ef database update
    ```

### Usage
To run the application locally:
```bash
dotnet run
