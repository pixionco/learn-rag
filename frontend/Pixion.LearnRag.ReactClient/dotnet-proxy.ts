import child_process from "child_process";
import fs from "fs";
import path from "path";
import { env } from "process";

export function createDotnetProxy() {
  const baseFolder =
    env.APPDATA !== undefined && env.APPDATA !== ""
      ? `${env.APPDATA}/ASP.NET/https`
      : `${env.HOME}/.aspnet/https`;

  fs.mkdirSync(baseFolder, { recursive: true });
  const certificateName = "learn-rag.client";
  const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
  const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

  if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (
      0 !==
      child_process.spawnSync(
        "dotnet",
        ["dev-certs", "https", "--export-path", certFilePath, "--format", "Pem", "--no-password"],
        { stdio: "inherit" }
      ).status
    ) {
      throw new Error(`Could not create certificate at: ${certFilePath} and ${keyFilePath}`);
    }
  }

  const target = env.ASPNETCORE_HTTPS_PORT
    ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
    : env.ASPNETCORE_URLS
      ? env.ASPNETCORE_URLS.split(";")[0]
      : "https://localhost:7189";

  return {
    proxy: {
      // can't be ^/api because it can conflict with other external services
      // e.g. sentry
      "^/learn-rag-api": {
        target,
        changeOrigin: true,
        secure: false,
      },
    },
    https: {
      key: fs.readFileSync(keyFilePath),
      cert: fs.readFileSync(certFilePath),
    },
  };
}
