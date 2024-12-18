import "@xyflow/react/dist/style.css";
import "highlight.js/styles/default.css";
import { CookieBanner } from "./components/CookieBanner";
import Interface from "./features/interface/Interface";
import "./lib/ga/ga";
import ReactHotToast from "./lib/react-hot-toast/ReactHotToast";
import ReactQueryProvider from "./lib/react-query/ReactQueryProvider";

function App() {
  return (
    <ReactQueryProvider>
      <Interface />
      <CookieBanner />
      <ReactHotToast />
    </ReactQueryProvider>
  );
}

export default App;
