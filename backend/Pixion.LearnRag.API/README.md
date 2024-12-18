## API Project

The `API Project` provides access to the application's functionality through REST endpoints. These endpoints are
implemented using Minimal API. Embedding-related endpoints can be enabled or disabled via the `EndpointsConfig`
configuration.

### Running

To run the application, at minimum, an `appsettings.json` file is required. For starter local development, you can:

1. Copy the contents of `appsettings.Example.json` to create your `appsettings.json` file.
2. Run the `docker-compose.yml` file located in the Infrastructure project to set up the database.

**Note:** By default, the project will use mocked AI model responses when running locally.

### SpaProxy

The project integrates the `SpaProxy` package, which enables a development environment for Single Page
Applications (SPAs) that can serve as the project's frontend.

`SpaProxy` acts as a bridge between the backend and the SPA during development. This setup enables live (hot) reloading
and other SPA development features.

Ensure the `SpaProxy` configuration (found in `Pixion.LearnRag.API.csproj`) in your project is correctly set up to point
to the SPA's development server.
