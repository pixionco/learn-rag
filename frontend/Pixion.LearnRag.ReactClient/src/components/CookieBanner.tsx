import { useState } from "react";
import { Button, DialogTrigger, Link } from "react-aria-components";
import DefaultDialog from "./dialog/DefaultDialog";
import DialogBody from "./dialog/DialogBody";
import DialogFooter from "./dialog/DialogFooter";
import DialogHeader from "./dialog/DialogHeader";

export function CookieBanner() {
  const [visible, setVisible] = useState(
    localStorage.getItem("cookieConsent") !== "1" && !!import.meta.env.VITE_GA_ID
  );

  function onConsent() {
    localStorage.setItem("cookieConsent", "1");
    setVisible(false);
  }

  if (!visible) return null;

  return (
    <DialogTrigger defaultOpen={visible}>
      <DefaultDialog overlay isKeyboardDismissDisabled proseWidth>
        <DialogHeader>Cookie Consent</DialogHeader>
        <DialogBody>
          <p className="max-w-prose font-medium text-neutral-700">
            We use cookies to improve your experience. To continue to browse the application, please
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
        </DialogBody>
        <DialogFooter>
          <Button
            className="flex w-full items-center justify-center gap-1 rounded-md bg-brand-500 p-2 font-semibold text-white"
            onPress={onConsent}
          >
            Got it
          </Button>
        </DialogFooter>
      </DefaultDialog>
    </DialogTrigger>
  );
}
