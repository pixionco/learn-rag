import { useQuery } from "@tanstack/react-query";
import { generateAnswer } from "../api";

export default function useGenerateAnswer(question?: string, context?: string) {
  return useQuery({
    queryKey: ["generate-answer"],
    queryFn: () => generateAnswer({ question: question!, context: context! }),
    enabled: !!question && !!context,
    refetchOnMount: false,
    refetchOnWindowFocus: false,
  });
}
