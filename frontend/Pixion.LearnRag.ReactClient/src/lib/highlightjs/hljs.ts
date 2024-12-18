import hljs from "highlight.js/lib/core";
import json from "highlight.js/lib/languages/json";
import "highlight.js/styles/intellij-light.css";

hljs.registerLanguage("json", json);

export default hljs;
