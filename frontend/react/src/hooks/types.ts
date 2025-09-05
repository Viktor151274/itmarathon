export interface FetchParams extends RequestInit {
  url: string;
}

export interface UseFetchReturn<T> {
  data: T | null;
  isLoading: boolean;
  isError: boolean;
  fetchData: () => Promise<void>;
}
