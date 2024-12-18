import { defineConfig } from "orval";

export default defineConfig({
  "learn-rag-client": {
    input: {
      target: "../../backend/Pixion.LearnRag.API/openapi.json",
    },
    output: {
      client: "axios-functions",
      workspace: "src/api/",
      target: "./endpoints",
      schemas: "./model",
      mode: "tags-split",
      clean: true,
      prettier: true,
      urlEncodeParameters: true,
      allParamsOptional: false,
      override: {
        useTypeOverInterfaces: true,
        mutator: {
          path: "./axios.ts",
        },
      },
    },
  },
});
