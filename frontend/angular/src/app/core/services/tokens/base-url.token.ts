import { InjectionToken } from '@angular/core';

import { environment } from '../../../../environments/environment';

export const BASE_URL = new InjectionToken<string>('BaseUrl', {
  providedIn: 'root',
  factory: () => {
    return environment.apiBackendUrl;
  },
});
