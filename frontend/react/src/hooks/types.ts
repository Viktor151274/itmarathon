export interface FetchParams extends RequestInit {
  url: string;
  onSuccess?: <T>(data: T) => void;
  onError?: (error: Error) => void;
}

export interface UseFetchReturn<T, N = undefined> {
  data: T | null;
  isLoading: boolean;
  isError: boolean;
  fetchData: (data: N) => Promise<void>;
}
