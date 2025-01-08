import * as Sentry from "@sentry/react";

const sentryDsn = import.meta.env.VITE_SENTRY_DSN;
if (sentryDsn) {
  Sentry.init({
    dsn: sentryDsn,
    integrations: [Sentry.browserTracingIntegration(), Sentry.replayIntegration()],
    // tracing
    tracesSampleRate: 1.0,
    tracePropagationTargets: ["localhost", /^https:\/\/pixion-learn-rag\.azurewebsites\.net\/api/],
    // session replay
    replaysSessionSampleRate: 0.1,
    replaysOnErrorSampleRate: 1.0,
  });
}
