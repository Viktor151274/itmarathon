import type { FetchParams } from "@hooks/types";

export interface RoomFormWizardProps<TResponse> {
  isAdmin?: boolean;
  fetchConfig: FetchParams<TResponse>;
}
