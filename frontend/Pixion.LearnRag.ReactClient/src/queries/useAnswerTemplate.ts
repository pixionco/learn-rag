import { useQuery } from "@tanstack/react-query";
import { getAnswerTemplate } from "../api";

export default function useAnswerTemplate() {
  return useQuery({
    queryKey: ["answer-template"],
    queryFn: getAnswerTemplate,
    refetchOnMount: false,
    refetchOnWindowFocus: false,
  });
}
