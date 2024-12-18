export type TableColumn<T> = {
  id: keyof T;
  name: string;
  isRowHeader?: boolean;
};
