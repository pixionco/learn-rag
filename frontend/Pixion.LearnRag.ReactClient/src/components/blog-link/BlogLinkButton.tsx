import External from "@/assets/icons/external.svg?react";
import { Link, type LinkProps } from "react-aria-components";
import GoogleAnalytics from "../../lib/ga/ga";
import { type PixionBlogString } from "./types";

type BlogLinkButtonProps = Omit<LinkProps, "href" | "target" | "rel" | "className"> & {
  href: PixionBlogString;
  text: string;
};

function BlogLinkButton({ href, text, ...props }: BlogLinkButtonProps) {
  return (
    <Link
      href={href}
      target="_blank"
      {...props}
      className="nodrag flex w-fit items-center gap-2 font-semibold text-brand-600 -mt-4"
      onPress={() => GoogleAnalytics.blogLinkEvent(href)}
    >
      {text}
      <External className="inline size-5 fill-brand-600" />
    </Link>
  );
}

export default BlogLinkButton;
