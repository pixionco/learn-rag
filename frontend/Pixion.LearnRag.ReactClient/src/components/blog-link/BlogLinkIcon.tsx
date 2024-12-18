import External from "@/assets/icons/external.svg?react";
import { Link, type LinkProps } from "react-aria-components";
import GoogleAnalytics from "../../lib/ga/ga";
import { type PixionBlogString } from "./types";

export type BlogLinkIconProps = Omit<LinkProps, "href" | "target" | "rel"> & {
  href: PixionBlogString;
};

function BlogLinkIcon({ href, ...props }: BlogLinkIconProps) {
  return (
    <Link
      href={href}
      target="_blank"
      {...props}
      onPress={() => GoogleAnalytics.blogLinkEvent(href)}
    >
      <External className="inline size-5 fill-brand-700" />
    </Link>
  );
}

export default BlogLinkIcon;
