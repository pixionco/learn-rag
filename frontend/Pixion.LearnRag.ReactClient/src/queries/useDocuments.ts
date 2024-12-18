import { useQuery } from "@tanstack/react-query";
import { getDocuments } from "../api";

export default function useDocuments() {
  return useQuery({
    queryKey: ["documents"],
    queryFn: getDocuments,
    refetchOnMount: false,
    refetchOnWindowFocus: false,
  });
}
