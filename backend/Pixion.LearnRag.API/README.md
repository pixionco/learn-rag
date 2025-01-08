## API Project

The `API Project` provides access to the application's functionality through REST endpoints. These endpoints are
implemented using Minimal API.

### Environment Variables / appsettings.json

To run the application in release configuration, at minimum, an `appsettings.json` file is required.
For the debug configuration, the already provided `appsettings.Local.json` should be enough, though all of the **AI related functionalities will be mocked**.

The `appsettings.Example.json` includes the full configuration object, which can be used to created the `appsettings.json`;

To enable running of mocked release configuration:

1. Create an `appsettings.json` file and copy the contents of `appsettings.Local.json` to it.

To enable running of non-mocked release configuration:

1. Create an `appsettings.json` file and copy the contents of `appsettings.Example.json` to it.
2. Change the placeholder values found in `appsettings.json` with actual values, this includes:
   - Changing the values of `AzureOpenAiChat` and `AzureOpenAiEmbedding` to valid values.
   - Optionally setting the `DatabaseConnection` in `DocumentDatabase` to an external Postgres database

To embed all of the documents found in `Infrastructure` project's `Assets` directory, set the `EmbedDocuments` flag to true.
Running the application multiple times with this setting turned on won't duplicate the work and/or data.

#### Environment Variables Explanations

- `SemanticKernel` hold the values needed to connect to external Azure OpenAI services. Currently this is the only supported provider for external LLM/Embedding services.
- `VectorDatabase` hold the values needed to connect to the vector database. Also allows for vector size and schema configuration.
- `DocumentDatabase` hold the values needed to oconnect to the document database. Can be the same as `VectorDatabase` or different.
- `Mock` hold the mocking related values.
  - `MockAiModels` enables mocking.
  - `UseFailingMocks` enables the mocks to fail.
- `Seed` hold the seeding related values.
  - `EmbedDocuments` enables startup-time embedding of all of the documents currently found in the database. How exactly the documents are embedded can be configured in the `DocumentEmbeddingSeeder` class. If the AI models are mocked the data won't make much sense. If using actual AI models beware that this process costs money.
- `Endpoints` hold the endpoints related values.
  - `DisableEmbedEndpoints` disables the embedding endpoints on the backend. This can be useful for Production environment to disable endpoint misuse.

### Running

The project can be run through one of the launch profiles defined in the `launchSettings.json` found in the `Properties` directory.

This can be done by running the following command:

```bash
dotnet run --launch-profile "PROFILE"
```

Where profile can be either `"https-local"`, `"https-development"` or `"https-production"`.
Different profiles change the `ASPNETCORE_ENVIRONMENT` value, which changes which `appsettings.ENVIRONMENT.json` file is loaded and part of the application logic (`app.Environment.IsProduction()` branches).

Make sure the local database container is running first when connecting to it.
