import { useState } from "react";
import { Button, Link } from "react-aria-components";

export function CookieBanner() {
  const [visible, setVisible] = useState(localStorage.getItem("cookieConsent") !== "1");

  function onConsent() {
    localStorage.setItem("cookieConsent", "1");
    setVisible(false);
  }

  if (!visible) return null;

  return (
    <div className="fixed inset-x-0 bottom-0 z-50 flex w-screen items-center justify-center gap-4 bg-white p-4 py-6">
      <p className="max-w-prose font-medium text-neutral-700">
        We use cookies to improve your experience. By continuing to browse the application, you
        accept our&nbsp;
        <Link
          href="https://pixion.co/cookie-policy"
          target="_blank"
          className="font-semibold underline"
        >
          Cookie Policy
        </Link>
        .
      </p>
      <Button
        onPress={onConsent}
        className="nodrag flex w-fit items-center gap-2 rounded-full bg-brand-500 px-6 py-2 text-xl font-semibold text-white transition-colors hover:bg-brand-600 disabled:cursor-not-allowed disabled:bg-neutral-300 disabled:text-neutral-100"
      >
        Got it
      </Button>
    </div>
  );
}
