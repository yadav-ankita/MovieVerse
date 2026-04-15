# MovieVerse

MovieVerse is a web application built with ASP.NET Core Razor Pages that allows users to browse, search, and view information about movies and TV seasons using The Movie Database (TMDB) API. The app supports user authentication, personal collections, and more.

## Features

- Browse popular movies and TV seasons
- Search for movies and seasons by title
- View detailed information, trailers, and overviews
- User authentication and registration using ASP.NET Core Identity
- Add movies/seasons to personal collections
- Responsive UI with Bootstrap
- Secure API key management using environment variables

## Getting Started

### Prerequisites

- [.NET 6.0+ SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/) (or update connection string for your DB)
- TMDB API Key ([Get one here](https://www.themoviedb.org/documentation/api))

### Setup

1. **Clone the repository:**
-git clone https://github.com/yadav-ankita/MovieVerse.git cd MovieVerse
2. **Set up environment variables:**
- Create a `.env` file in the project root:
     ```
     TMDB_API_KEY=your_tmdb_api_key_here
     ```
   - Add `.env` to your `.gitignore` if not already present.

3. **Configure the database:**
   - Update your connection string in `appsettings.json` or via environment variables.

4. **Install dependencies:**
dotnet restore

5. **Apply database migrations:**
dotnet ef database update

6. **Run the application:**
dotnet run

7. **Access the app:**
- Open your browser and go to `https://localhost:5001` (or the port shown in your terminal).

## Project Structure

- `Controllers/` - MVC controllers for handling requests
- `Models/` - Entity and view models
- `Views/` - Razor views for UI
- `wwwroot/` - Static files (CSS, JS, images)
- `Configurations/` - App settings and configuration classes
- `Migrations/` - Entity Framework Core migrations

## Security
- **Authentication:** Uses ASP.NETCore Identity for user management.

## Contributing

Pull requests are welcome! For major changes,please openan issue first to discuss what you would like tochange.

## License

[MIT](LICENSE)

---

**Developedby Ankita Yadav**