import { QueryCache, QueryClient, QueryClientProvider } from "@tanstack/react-query";
import "@xyflow/react/dist/style.css";
import { type AxiosError } from "axios";
import "highlight.js/styles/default.css";
import { type PropsWithChildren } from "react";
import toast, { ErrorIcon } from "react-hot-toast";
import { type LearnRAGError } from "../../types/rag";
import AxiosErrorToast from "../react-hot-toast/AxiosErrorToast";

declare module "@tanstack/react-query" {
  interface Register {
    defaultError: AxiosError<LearnRAGError>;
  }
}

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      gcTime: Infinity,
      refetchOnWindowFocus: false,
    },
  },
  queryCache: new QueryCache({
    onError: error => {
      toast(t => <AxiosErrorToast t={t} error={error} />, {
        icon: <ErrorIcon className="shrink-0" />,
      });
    },
  }),
});

function ReactQueryProvider({ children }: PropsWithChildren) {
  return <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>;
}

export default ReactQueryProvider;
