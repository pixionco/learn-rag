ARG DOTNET_VERSION=9.0
ARG NODE_VERSION=22

## ---------------------------------------------------------------------------------- ##
## -------------------------------- base -------------------------------------------- ##
## ---------------------------------------------------------------------------------- ##
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS base
RUN apt-get update \
  && apt-get upgrade -y \
  && apt-get install -y --no-install-recommends git curl \
  && apt-get autoremove -y \
  && apt-get clean -y \
  && rm -rf /root/.cache \
  && rm -rf /var/apt/lists/* \
  && rm -rf /var/cache/apt/* \
  && apt-get purge -y --auto-remove -o APT::AutoRemove::RecommendsImportant=false\
  && mkdir -p /workspace \
  && curl -fsSL https://deb.nodesource.com/setup_${NODE_VERSION}.x | bash -\
  && apt-get install -y nodejs npm \
  && node --version \
  && npm --version; 

## ---------------------------------------------------------------------------------- ##
## --------------------------- base with dependencies ------------------------------- ##
## ---------------------------------------------------------------------------------- ##
FROM base AS base-with-dependencies

WORKDIR /workspace

# Copy source
COPY ./ ./

# Install Dot net Dependencies
RUN dotnet restore
RUN dotnet tool restore

WORKDIR /workspace/frontend/Pixion.LearnRag.ReactClient
COPY ./frontend/Pixion.LearnRag.ReactClient/package*.json ./
RUN npm ci 

## ---------------------------------------------------------------------------------- ##
## ---------------------------------- prod ------------------------------------------ ##
## ---------------------------------------------------------------------------------- ##
FROM base-with-dependencies AS prod-start

ARG VITE_GA_ID
ARG VITE_GA_DEBUG
ARG VITE_SENTRY_DSN
ARG SENTRY_ORG
ARG SENTRY_PROJECT
ARG SENTRY_AUTH_TOKEN

WORKDIR /workspace

ENV VITE_GA_ID=${VITE_GA_ID}
ENV VITE_GA_DEBUG=${VITE_GA_DEBUG}
ENV VITE_SENTRY_DSN=${VITE_SENTRY_DSN}
ENV SENTRY_ORG=${SENTRY_ORG}
ENV SENTRY_PROJECT=${SENTRY_PROJECT}
ENV SENTRY_AUTH_TOKEN=${SENTRY_AUTH_TOKEN}

# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS prod-image

WORKDIR /app

COPY --from=prod-start /workspace/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Pixion.LearnRag.API.dll"]