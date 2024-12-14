# Client Management Application API Documentation

## Introduction
The **Client Management Application API** is a robust backend system designed to manage and organize client data for businesses. It enables seamless CRUD operations to create, retrieve, update, and delete client information in a secure and scalable manner.

## Features
- CRUD operations for managing client data
- Authentication and authorization mechanisms
- RESTful API architecture
- Integration with database for persistent storage
- Robust Search Functionality
- Error handling and logging for improved debugging

## Technologies Used
- **.NET 7** - Framework for building the API
- **Entity Framework Core** - ORM for database interaction
- **SQL Server/PostgreSQL** - Database
- **Swagger** - API documentation and testing
- **JWT** - JSON Web Tokens for authentication

## Getting Started

### Prerequisites
To get started with this project, you will need the following:
- .NET SDK (version 7 or higher)
- SQL Server for database management
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
```
### Folder Structure
The Folder Structure is organized as follows:
```bash
ClientMgmtApp/
    │
    └── ClientMgmtAppAPI.Common/      # Constants, Models, Utilities to be used across the Solution
    |        ├── Constants # ApiRoutes, Messages, etc
    |        ├── Models
    |        └── Utilities # General Purpose Methods that will be helpful throughout the Solution
    └── ClientMgmtAppAPI.Models/      # DTOs, Entities and other necessary schemas used throughout the Solution
    |        ├── DtoModels # DTOs used throughout the Solution
    |        └── Entities  # Classes used for Schema Structure for the ORM to use in the DB Server
    └── ClientMgmtAppAPI.Services/      # Business Logic & Logic Powerhouse of the Solution
    |        ├── Data # Database Context for ORM Interaction
    |        ├── IServices  # Interface(s) for the Services of the Solution
    |        ├── Migrations # Migration Classes for ORM-DB Interactions
    |        └── Services # Main Business Logic of the Application
    └── ClientMgmtAppAPI/      # API Endpoints & Execution
           ├── Controllers # HTTP endpoints
           └── Properties # launchsettings, etc
```
