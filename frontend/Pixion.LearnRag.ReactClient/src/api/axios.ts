import Axios, { type AxiosRequestConfig } from "axios";

const axios = Axios.create();

export default async function customInstance<T>(
  config: AxiosRequestConfig,
  options?: AxiosRequestConfig
): Promise<T> {
  const { data } = await axios<T>({ ...config, ...options });
  return data;
}
