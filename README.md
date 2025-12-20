# EntityFramework

A small collection of utilities, patterns, and helpers to simplify working with Entity Framework in .NET projects. This repository provides reusable components, examples, and best-practice patterns to make data access easier, more maintainable, and testable.

## Key features
- Lightweight helpers for common Entity Framework tasks (queries, migrations, seeding).
- Reusable repository and unit-of-work patterns.
- Utilities to simplify DbContext configuration and lifecycle management.
- Example projects and tests demonstrating common scenarios.

## Getting started
1. Clone the repository:
   `git clone https://github.com/migueljocode/EntityFramework.git`
2. Open the solution in Visual Studio or your preferred IDE.
3. Restore NuGet packages and build the solution.

## Installation
Reference the relevant project or library from your application. If packaged to NuGet (TBD), install via:
`dotnet add package <PackageName>`

## Quick example
A minimal example showing how to register and use a DbContext:
- Configure services in your `Startup`/`Program`:
`services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));`
- Use a repository or DbContext in your application code to perform CRUD operations.

## Contributing
Contributions are welcome. Please:
- Open an issue for bugs or feature requests.
- Send a pull request with tests and clear descriptions for changes.
- Follow the coding conventions present in the repository.

## License
This project is available under the [MIT License](LICENSE) — or update with the appropriate license.

## Contact
Maintainer: migueljocode (GitHub)
