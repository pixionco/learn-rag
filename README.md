# Pixion Learn RAG

Pixion Learn RAG is a project developed using Visual Studio's React and ASP.NET Core template. 
It demonstrates the data flow and logic of the Retrieval-Augmented Generation (RAG) strategies mentioned in our [blog post series](https://pixion.co/blog/introducing-pixion-blog-series-on-rag-llms). 
The project is divided into two main parts: the backend and the frontend.

The backend follows Clean Architecture principles and is split into four projects: `Core`, `Application`, `Infrastructure`, and `API`. 
The frontend is implemented inside a single project called `ReactClient`, showcasing the data flow in an interactive, graph-like sandbox.

Instructions for running the project can be found in the backend’s `API Project`. 

Below is an overview of the implemented strategies with brief descriptions. 
Additional tables for embedding options and retrieval options can be found in the [Extra](#extra) section, which may help with project navigation.

## Strategies
Several RAG strategies are implemented in the project:

### Basic [#](https://pixion.co/blog/basic-index-retrieval)
The basic strategy splits a document into chunks, which are sent to an embedding model to generate embeddings. 
The chunks, metadata and embeddings are then saved in the database, supporting vector search.

When user poses a question, a vector similarity search is performed and the most relevant chunks are combined into
context which is then provided to the LLM to generate a final answer.

### Sentence Window [#](https://pixion.co/blog/rag-strategies-context-enrichment)
In contrast to the basic strategy, the sentence window strategy considers neighboring chunks around the relevant chunk after the search. 
The number of neighboring chunks is determined by the `range` property. 
For example, if chunk index `5` is relevant and `range` is set to `2`, chunks with indexes `3`, `4` (upper neighbors) and `6`, `7` (lower neighbors) will be used to create context.

### Auto Merging [#](https://pixion.co/blog/rag-strategies-context-enrichment)
This strategy performs chunking twice: first with a higher chunk size (creating `parentChunks`) and then with a smaller chunk size (creating `childChunks`). 
In theory this strategy supports any number of levels, but for sake of simplicity we kept it at 2.
Both chunk types are embedded. When a search request is made, child chunks are searched first. 
Afterward, if the number of child chunks passes a threshold they are merged together into the parent chunk they were derived from.

### Hierarchical [#](https://pixion.co/blog/rag-strategies-hierarchical-index-retrieval)
Similar to the auto merging strategy, chunking is performed twice in this approach (but also supports more levels). 
However, in the hierarchical strategy, the size difference between the parent and child chunks can be significant, so the parent chunks are summarized. 
For example, the parent chunks might represent entire sections or even the whole document at the first level.
For the purpose of this project, the scale was kept small.

In the first stage of the search (which returns `limit` chunks), a summary of the parent chunks is used for the vector search. 
After that, the `child_limit` chunks are searched within each of the selected parent chunks, allowing for a more granular search in the second stage.

### Hypothetical Question [#](https://pixion.co/blog/rag-strategies-hypothetical-questions-hyde)
The Hypothetical Question strategy generates one or more questions from each chunk of a document using an LLM, 
which are then embedded and stored in a vector database. 
During retrieval, the user's query is compared to these hypothetical question embeddings rather than the chunk embeddings. 
This approach improves semantic alignment between user queries and document chunks, 
with metadata linking the questions back to their respective chunks for accurate context retrieval.

## Extra

Here is a table that shows embedding options properties per strategy:

|                     | basic | sentence window | auto merging | hierarchical | hypothetical question |
|---------------------|:-----:|:---------------:|:------------:|:------------:|:---------------------:|
| chunk size          |   ✔   |        ✔        |      ✔       |      ✔       |           ✔           |
| chunk overlap       |   ✔   |                 |      ✔       |      ✔       |           ✔           |
| child chunk size    |       |                 |      ✔       |      ✔       |                       |
| child chunk overlap |       |                 |      ✔       |      ✔       |                       |
| number of questions |       |                 |              |              |           ✔           |

Here is a table that shows retrieval options properties per strategy:

|                                | basic | sentence window | auto merging | hierarchical | hypothetical question |
|--------------------------------|:-----:|:---------------:|:------------:|:------------:|:---------------------:|
| range                          |       |        ✔        |              |              |                       |
| child parent prevalence factor |       |                 |      ✔       |              |                       |
| limit                          |   ✔   |        ✔        |      ✔       |      ✔       |           ✔           |
| child limit                    |       |                 |              |      ✔       |                       |
