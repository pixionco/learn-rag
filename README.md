# Pixion Learn RAG

Pixion Learn RAG is a project developed using [Visual Studio's React and ASP.NET Core template](https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-asp-net-core-with-react?view=vs-2022).
It demonstrates the data flow and logic of the Retrieval-Augmented Generation (RAG) strategies mentioned in our [blog post series about RAG and LLMs](https://pixion.co/blog/introducing-pixion-blog-series-on-rag-llms)
and is also part of [Enterprise RAG AI Solutions Case Study](https://pixion.co/work/enterprise-rag-ai-solutions#:~:text=Introducing%20the%20Learn%20RAG%20educational%20application)

The application can be publicly acessed on: https://learn-rag.pixion.co/

The project is divided into two main parts: the backend and the frontend:

- The frontend is implemented inside a single project called `ReactClient`, showcasing the data flow in an interactive, graph-like sandbox.
- The backend follows clean architecture principles and is split into four projects: `Core`, `Application`, `Infrastructure`, and `API`.

## Running the Project

Before running project in any of the configurations, it's necessary to make sure the environment variables are properly setup.

Frontend variables can be loaded by creating an `.env` file in the `/frontend/Pixion.LearnRag.ReactClient` directory.
All **frontend environment variables are optional**, and are not required for the project's functionality.
More on the frontend variables can be read in `ReactClient` project's README file.

Backend variables can be loaded by creating an `appsettings.json` files in the `/backend/Pixion.LearnRag.API` directory.
**Backend environment variables are required** for the project to function properly.
More on the backend variables can be read in `API` project's README file.

### Database

While the project can be run without the database, the application will be unusable as no data can be fetched.
Before runing the application make sure that the either local or external databases are running and that the right environment variables are set.

The local database can be started manually by running the following command from the root of the project:

```bash
docker-compose -f .\backend\Pixion.LearnRag.Infrastructure\docker-compose.yml up -d
```

The container wont be automatucally stopped when the applications stops. This needs to be done manually by running the following command from the root of the project:

```bash
docker-compose -f .\backend\Pixion.LearnRag.Infrastructure\docker-compose.yml stop
```

### Release/Production

If you simply want to run the project without any modification, the easiest way is to run the release version of the application through the `docker-compose.yml`.

You can achieve that by running the following command from the root of the project:

```bash
docker-compose up
```

**Note**: You need to create the `appsettings.json` file in the `API` project for the image to work properly.

### Debug/Development

If you want to run the project in development mode, the easiest way is to use one of the launch profiles defined in the `API` project.

To start the application in `Local` environment (local database running in docker), you can run the following command from the root of the project:

```bash
dotnet run --project "./backend/Pixion.LearnRag.API/" --launch-profile "https-local"
```

More details can be found in the `API` project's README file.

## Extra

Below is an overview of the implemented strategies with brief descriptions.
Additional tables for embedding options and retrieval options can be found in the [Strategy Table](###strategy-table) section, which may help with project navigation.

### Basic [https://pixion.co/blog/basic-index-retrieval](https://pixion.co/blog/basic-index-retrieval)

The basic strategy splits a document into chunks, which are sent to an embedding model to generate embeddings.
The chunks, metadata and embeddings are then saved in the database, supporting vector search.

When user poses a question, a vector similarity search is performed and the most relevant chunks are combined into
context which is then provided to the LLM to generate a final answer.

### Sentence Window [https://pixion.co/blog/rag-strategies-context-enrichment](https://pixion.co/blog/rag-strategies-context-enrichment)

In contrast to the basic strategy, the sentence window strategy considers neighboring chunks around the relevant chunk after the search.
The number of neighboring chunks is determined by the `range` property.
For example, if chunk index `5` is relevant and `range` is set to `2`, chunks with indexes `3`, `4` (upper neighbors) and `6`, `7` (lower neighbors) will be used to create context.

### Auto Merging [https://pixion.co/blog/rag-strategies-context-enrichment](https://pixion.co/blog/rag-strategies-context-enrichment)

This strategy performs chunking twice: first with a higher chunk size (creating `parentChunks`) and then with a smaller chunk size (creating `childChunks`).
In theory this strategy supports any number of levels, but for sake of simplicity we kept it at 2.
Both chunk types are embedded. When a search request is made, child chunks are searched first.
Afterward, if the number of child chunks passes a threshold they are merged together into the parent chunk they were derived from.

### Hierarchical [https://pixion.co/blog/rag-strategies-hierarchical-index-retrieval](https://pixion.co/blog/rag-strategies-hierarchical-index-retrieval)

Similar to the auto merging strategy, chunking is performed twice in this approach (but also supports more levels).
However, in the hierarchical strategy, the size difference between the parent and child chunks can be significant, so the parent chunks are summarized.
For example, the parent chunks might represent entire sections or even the whole document at the first level.
For the purpose of this project, the scale was kept small.

In the first stage of the search (which returns `limit` chunks), a summary of the parent chunks is used for the vector search.
After that, the `child_limit` chunks are searched within each of the selected parent chunks, allowing for a more granular search in the second stage.

### Hypothetical Question [https://pixion.co/blog/rag-strategies-hypothetical-questions-hyde](https://pixion.co/blog/rag-strategies-hypothetical-questions-hyde)

The Hypothetical Question strategy generates one or more questions from each chunk of a document using an LLM,
which are then embedded and stored in a vector database.
During retrieval, the user's query is compared to these hypothetical question embeddings rather than the chunk embeddings.
This approach improves semantic alignment between user queries and document chunks,
with metadata linking the questions back to their respective chunks for accurate context retrieval.

### Strategy Table

Table that shows embedding options properties per strategy:

|                     | basic | sentence window | auto merging | hierarchical | hypothetical question |
| ------------------- | :---: | :-------------: | :----------: | :----------: | :-------------------: |
| chunk size          |   ✔   |        ✔        |      ✔       |      ✔       |           ✔           |
| chunk overlap       |   ✔   |                 |      ✔       |      ✔       |           ✔           |
| child chunk size    |       |                 |      ✔       |      ✔       |                       |
| child chunk overlap |       |                 |      ✔       |      ✔       |                       |
| number of questions |       |                 |              |              |           ✔           |

Table that shows retrieval options properties per strategy:

|                                | basic | sentence window | auto merging | hierarchical | hypothetical question |
| ------------------------------ | :---: | :-------------: | :----------: | :----------: | :-------------------: |
| range                          |       |        ✔        |              |              |                       |
| child parent prevalence factor |       |                 |      ✔       |              |                       |
| limit                          |   ✔   |        ✔        |      ✔       |      ✔       |           ✔           |
| child limit                    |       |                 |              |      ✔       |                       |
