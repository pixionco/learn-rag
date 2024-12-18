## Infrastructure Project

The `Infrastructure Project` focuses on managing communication with external services (e.g., OpenAI) and handling data
storage, specifically using a PostgreSQL database. Additionally, it contains static assets, such as documents intended
for embedding.

### Seeders

Seeders are located in the **Seeders** folder and serve to initialize necessary database tables and data:

- **DocumentTableSeeder** and **StrategyTableSeeder** ensure the required tables for the application are created and
  ready for use.
- **DocumentEmbeddingSeeder** can be enabled or disabled via configuration. This seeder facilitates embedding documents
  from the **assets** folder based on preconfigured `EmbeddingOptions`, which are defined within the same class.

### Clients

The project also includes **client mocks**, which can be enabled via configuration. While mocks allow the application
flow to function, they do not produce accurate results and should only be used for testing purposes.

### Docker

The `Infrastructure Project` includes a `docker-compose.yml` file and an `init.sql` script to allow local application
development by providing a PostgreSQL replacement.

The database can be run by running the following command:

```bash 
docker compose up
```

Ensure that the configuration in `API Project` is set up to connect to the local database for the application to work.

