
# Clean Architecture API Solution Template
![.NET Core](https://github.com/jasontaylordev/CleanArchitecture/workflows/.NET%20Core/badge.svg) [![Clean.Architecture.Solution.Template NuGet Package](https://img.shields.io/badge/nuget-1.1.1-blue)](https://www.nuget.org/packages/Clean.Architecture.Solution.Template) [![NuGet](https://img.shields.io/nuget/dt/Clean.Architecture.Solution.Template.svg)](https://www.nuget.org/packages/Clean.Architecture.Solution.Template) [![Twitter Follow](https://img.shields.io/twitter/follow/jasontaylordev.svg?style=social&label=Follow)](https://twitter.com/jasontaylordev)

<br/>

This is a solution template for creating a ASP.NET Core API service following the principles of Clean Architecture. Create a new project based on this template by clicking the above **Use this template** button or by installing and running the associated NuGet package (see Getting Started for full details). 

## Technologies

* ASP.NET Core 5
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)
* [Docker](https://www.docker.com/)

## Getting Started

1. Install the latest [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Position yourself into the repository root
3. Run `dotnet new --install .` to install the project template
4. Run `dotnet new --install sc-api-ca -n SampleProject -o .\SampleProject` to create a new project
5. Or optionally, create a folder for your solution and cd into it (the template will use it as project name)
6. Navigate to `src/SampleProject.Web` and run `dotnet run` to launch the back end (ASP.NET Core Web API)

### Docker Configuration

In order to build and run the docker containers, execute `docker-compose -f 'docker-compose.yml' up --build` from the root of the solution where you find the docker-compose.yml file.  You can also use "Docker Compose" from Visual Studio for Debugging purposes.
Then open http://localhost:5006/api in your browser.

### Database Configuration

The template is configured to use PostgreSQL database by default thats running in Docker. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. installing PostgreSQL). If you would like to use a different database, verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid database server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

### Database Migrations

To use `dotnet-ef` for your migrations please add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure` (optional if in this folder)
* `--startup-project src/<YOUR_PROJECT_NAME>.Web`
* `--output-dir Persistence/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\SC.API.CleanArchitecture.Web --output-dir Persistence\Migrations`

If you're using Package Manager Console, running the migrations is a bit different:

`Add-Migration -Name "SampleMigration" -Project "src\Infrastructure" -Context "ApplicationDbContext" -StartupProject "src\SC.API.CleanArchitecture.Web" -OutputDir "Persistence\Migrations" -Verbose`

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### Web

This layer is a REST API based on ASP.NET Core 5. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

## Support

If you are having problems, please let us know by [raising a new issue](https://github.com/jasontaylordev/CleanArchitecture/issues/new/choose).

## License

This project is licensed with the [MIT license](LICENSE).