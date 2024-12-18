// export type ValueOption = {
//   id: number | string;
//   name: string;
// };

export type ValueOption<T = unknown> = {
  id: number | string;
  name: string;
  value?: T;
};
