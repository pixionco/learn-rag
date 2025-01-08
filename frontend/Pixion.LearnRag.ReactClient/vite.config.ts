import { sentryVitePlugin } from "@sentry/vite-plugin";
import react from "@vitejs/plugin-react";
import { fileURLToPath, URL } from "node:url";
import { defineConfig, loadEnv } from "vite";
import svgr from "vite-plugin-svgr";
import { createDotnetProxy } from "./dotnet-proxy";

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
  process.env = {...process.env, ...loadEnv(mode, process.cwd(), '')};

  return {
    plugins: [
      react({
        babel: {
          plugins: [
            [
              "babel-plugin-react-compiler",
              {
                sources: (filename: string) => {
                  return filename.indexOf("src/") !== -1;
                },
                target: "18",
              },
            ],
          ],
        },
      }),
      svgr({
        svgrOptions: {
          plugins: ["@svgr/plugin-svgo", "@svgr/plugin-jsx"],
          svgoConfig: {
            floatPrecision: 2,
          },
        },
        include: "**/*.svg?react",
      }),
      sentryVitePlugin({
        org: process.env.SENTRY_ORG,
        project: process.env.SENTRY_PROJECT,
        authToken: process.env.SENTRY_AUTH_TOKEN,
      }),
    ],
    resolve: {
      alias: {
        "@": fileURLToPath(new URL("./src", import.meta.url)),
      },
    },
    server: {
      port: 8080,
      ...createDotnetProxy(),
    },
    build: {
      sourcemap: true,
    },
  }
});
