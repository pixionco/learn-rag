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
      className="nodrag flex w-fit items-center gap-2 rounded-full bg-brand-500 px-4 py-1 font-semibold text-white transition-colors hover:bg-brand-600"
      onPress={() => GoogleAnalytics.blogLinkEvent(href)}
    >
      {text}
      <External className="inline size-5 fill-white" />
    </Link>
  );
}

export default BlogLinkButton;
