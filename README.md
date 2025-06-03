# Security Application

A sample security application with ASP.NET Core Web API, responsible for the main web system using the same domain network. When starting using the deployment command in Docker, we will have system virtualization for the local application development environment.

## Using the Angular with ASP.NET Core

The ASP.NET Core with Angular project template provides a convenient starting point for ASP.NET Core apps using Angular and the Angular CLI to implement a rich, client-side user interface (UI).

The project template is equivalent to creating both an ASP.NET Core project to act as a web API and an Angular CLI project to act as a UI. This project combination offers the convenience of hosting both projects in a single ASP.NET Core project that can be built and published as a single unit.

The project template isn't meant for server-side rendering (SSR).

## Command deployed application in Docker

In a terminal on the application path run the command:
``` bash
docker-compose up -d
```

``` bash
chmod +x start.sh
```

``` bash
./start.sh 
```

## Admin Credentials
**Username:** `admin`

**Password:** `admin`


*Note:* Credentials can be added or changed in the [AppDbContext](https://github.com/abelgasque/AbelGasque.WebApp.SecurityApp/tree/main/Server/Infrastructure/Entities/Context/AppDbContext.cs) file and generating a new migration before deploying to the development environment.

## References

[Use the Angular project template with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/client-side/spa/angular?view=aspnetcore-7.0&tabs=visual-studio)