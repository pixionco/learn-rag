type ErrorMessageProps = {
  message?: string;
};

function ErrorMessage({ message }: ErrorMessageProps) {
  if (!message) {
    return null;
  }

  return <p className="text-state-invalid">{message}</p>;
}

export default ErrorMessage;
