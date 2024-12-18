/* eslint-disable @typescript-eslint/no-explicit-any */

export type SatisfiesOne<T> = T extends any
  ? { [K in keyof T]: Required<Pick<T, K>> & Partial<Omit<T, K>> }[keyof T]
  : never;

export type Require<TObject, TField extends keyof TObject> = Omit<TObject, TField> &
  Required<Pick<TObject, TField>>;
