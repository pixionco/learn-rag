import { type StrategyTab } from "./types";

export const strategyTabs: StrategyTab[] = [
  {
    id: "basic",
    title: "Basic",
    href: "https://pixion.co/blog/basic-index-retrieval",
    description:
      "The basic strategy chunks the document and returns the most relevant chunks for a given question.",
  },
  {
    id: "sentence-window",
    title: "Sentence Window",
    href: "https://pixion.co/blog/rag-strategies-context-enrichment",
    description:
      "The sentence window strategy fetches additional neighboring chunks around the found most relevant ones, and combines them together.",
  },
  {
    id: "auto-merging",
    title: "Auto-Merging",
    href: "https://pixion.co/blog/rag-strategies-context-enrichment#auto-merging-retrieval",
    description:
      "The auto-merging strategy chunks the document hierarchically. Lowest level chunks are searched, and if enough of them originate from the same parent, they are recursively merged into their respective parent chunks.",
  },
  {
    id: "hierarchical",
    title: "Hierarchical",
    href: "https://pixion.co/blog/rag-strategies-hierarchical-index-retrieval",
    description:
      "The hierarchical strategy chunks the document hierarchically. Upper hierarchy levels are summarised. Retrieval is done iteratively based on summaries, allowing us to narrow down to the relevant chunks.",
  },
  {
    id: "hypothetical-question",
    title: "Hypothetical Question",
    href: "https://pixion.co/blog/rag-strategies-hypothetical-questions-hyde",
    description:
      "The hypothetical question strategy creates questions from the chunks content. Chunks are searched based on generated questions instead of content.",
  },
] as const;
