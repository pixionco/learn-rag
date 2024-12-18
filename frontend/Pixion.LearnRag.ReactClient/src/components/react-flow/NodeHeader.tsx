import { memo, type PropsWithChildren } from "react";
import { Header, Heading, TooltipTrigger } from "react-aria-components";
import BlogLinkIcon from "../blog-link/BlogLinkIcon";
import { type PixionBlogString } from "../blog-link/types";
import DefaultTooltip from "../DefaultTooltip";

type NodeHeaderProps = PropsWithChildren<{
  title: string;
  blogLinkHref?: PixionBlogString;
}>;

const NodeHeader = memo<NodeHeaderProps>(function NodeBase({
  title,
  blogLinkHref,
  children,
}: NodeHeaderProps) {
  return (
    <Header className="flex h-12 w-full items-center justify-between gap-20 rounded-t-xl border-b border-neutral-300 bg-white px-4 py-2">
      <div className="flex items-center gap-2">
        <Heading className="font-semibold text-neutral-800">{title}</Heading>
        {blogLinkHref && (
          <TooltipTrigger delay={200} closeDelay={100}>
            <BlogLinkIcon href={blogLinkHref} />
            <DefaultTooltip>View more at our blog</DefaultTooltip>
          </TooltipTrigger>
        )}
      </div>
      <div className="nodrag flex items-center gap-4">{children}</div>
    </Header>
  );
});

export default NodeHeader;
