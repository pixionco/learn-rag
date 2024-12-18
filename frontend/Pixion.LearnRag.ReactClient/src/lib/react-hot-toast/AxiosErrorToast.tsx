import { type AxiosError } from "axios";
import toast, { type Toast } from "react-hot-toast";
import { type LearnRAGError } from "../../types/rag";

type AxiosErrorToastProps = {
  t: Toast;
  error: AxiosError<LearnRAGError>;
};

function AxiosErrorToast({ t, error }: AxiosErrorToastProps) {
  return (
    <div
      onClick={() => toast.dismiss(t.id)}
      className="flex w-fit cursor-pointer flex-col gap-2 pl-1"
    >
      <header className="font-semibold text-neutral-700">
        {error.response?.data.title ?? "Unexpected error occured"}
      </header>
      <p className="text-neutral-500">
        {error.response?.data.detail ??
          "Most likely the server is down. Please try again in few minutes."}
      </p>
    </div>
  );
}

export default AxiosErrorToast;
