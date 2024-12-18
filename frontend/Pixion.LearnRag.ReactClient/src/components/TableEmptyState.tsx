import DatabaseOff from "@/assets/icons/database-off.svg?react";
import ProgressCircle from "@/assets/icons/progress-circle.svg?react";

type TableEmptyStateProps = {
  loading: boolean;
};

export function TableEmptyState({ loading }: TableEmptyStateProps) {
  if (loading) {
    return (
      <div className="flex items-center justify-center p-12">
        <div className="flex items-center gap-6">
          <ProgressCircle className="size-20 shrink-0 animate-spin fill-neutral-700" />
        </div>
      </div>
    );
  }

  return (
    <div className="flex items-center justify-center p-12 px-24">
      <div className="flex items-center gap-6">
        <DatabaseOff className="size-20 shrink-0 fill-neutral-700" />
        <p className="text-xl text-neutral-700">
          There is not enough information to fetch the data
        </p>
      </div>
    </div>
  );
}
