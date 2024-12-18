import { type PixionBlogString } from "../../components/blog-link/types";
import { type CanvasControls } from "../../stores/application-settings-store";
import { type RAGStrategy } from "../../stores/rag-options-store";

const GA_ID = import.meta.env.VITE_GA_ID;
const initialized = !!GA_ID;

const GoogleAnalytics = {
  strategyChangeEvent(strategy: RAGStrategy) {
    if (!initialized) return;

    gtag("event", "rag_strategy_change", {
      strategy,
      description: "Strategy was changed through side menu",
    });
  },
  blogLinkEvent(href: PixionBlogString) {
    if (!initialized) return;

    gtag("event", "blog_link_clicked", {
      href,
      description: "Blog link was visited",
    });
  },
  canvasControlsEvent(controls: CanvasControls) {
    if (!initialized) return;

    gtag("event", "canvas_controls_changed", {
      controls,
      description: "Canvas controls were changed",
    });
  },
};

export default GoogleAnalytics;
