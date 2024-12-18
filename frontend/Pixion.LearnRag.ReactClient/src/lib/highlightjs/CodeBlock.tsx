/* eslint-disable tailwindcss/no-custom-classname */
import { memo, useEffect, useRef } from "react";
import hljs from "./hljs";

type CodeBlockProps = {
  code: string;
  language: string;
};

const CodeBlock = memo<CodeBlockProps>(function CodeBlock({ code, language }) {
  const codeRef = useRef<HTMLElement>(null);

  // refs should always be set before the useEffect is run
  useEffect(() => {
    if (codeRef) hljs.highlightElement(codeRef.current!);
  }, []);

  return (
    <pre>
      <code className={`language-${language}`} ref={codeRef} style={{ background: "transparent" }}>
        {code}
      </code>
    </pre>
  );
});

export default CodeBlock;
