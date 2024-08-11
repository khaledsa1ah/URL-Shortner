# URL Shortener

This is a URL Shortener project built using .NET 8.0 with a minimal API approach. The project allows users to shorten long URLs and retrieve the original URLs using the shortened versions. The application uses Entity Framework Core with SQL Server for data storage.


## Features

- **Shorten URLs**: Allows users to submit long URLs and generate shortened versions.
- **Retrieve Original URLs**: Users can use the shortened URLs to redirect to the original URLs.
- **Swagger Documentation**: The API endpoints are documented using Swagger for easy testing and exploration during development.
- **HTTPS Redirection**: The application enforces HTTPS to ensure secure communication.

## Technologies Used

- ASP.NET Core 8.0
- Minimal APIs
- Entity Framework Core 9.0 (preview)
- SQL Server
- Swagger / OpenAPI

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server

### Installation

1. Clone the repository:
git clone https://github.com/yourusername/URL-Shortner.git

2. Navigate to the project directory:
cd URL-Shortner
3. Update the connection string in `appsettings.json` to point to your SQL Server instance.

4. Run the Entity Framework migrations to create the database:
dotnet ef database update
 
5. Run the application:
dotnet run
 
6. Open a web browser and navigate to `https://localhost:5001/swagger` to view the Swagger UI.

## Usage

### Shorten a URL

Send a POST request to `/shorturl` with the following JSON body:

```json
{
"originalURL": "https://www.example.com/very/long/url/that/needs/shortening"
}
```
The service will return a shortened URL.
Access a shortened URL
Simply navigate to the shortened URL in your web browser, and you will be redirected to the original URL.

## API Endpoints

- POST /shorturl: Create a new shortened URL
- GET /{shortCode}: Redirect to the original URL (handled by the fallback route)

