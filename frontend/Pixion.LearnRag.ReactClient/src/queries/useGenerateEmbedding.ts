import { useQuery } from "@tanstack/react-query";
import { generateEmbedding } from "../api";

export default function useGenerateEmbedding(text?: string) {
  return useQuery({
    queryKey: ["embedding", text],
    queryFn: () => generateEmbedding({ text: text! }),
    enabled: !!text,
    refetchOnMount: false,
    refetchOnWindowFocus: false,
  });
}
