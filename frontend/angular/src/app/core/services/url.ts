import { inject, Injectable } from '@angular/core';
import { WINDOW } from './tokens/window.token';

@Injectable({ providedIn: 'root' })
export class UrlService {
  private readonly window = inject(WINDOW);

  build(userCode: string, segment = 'join') {
    const code = encodeURIComponent(userCode.trim());

    const routerPath = `/${segment}/${code}`;
    const origin = this.window.location.origin;
    const absoluteUrl = `${origin}${routerPath}`;

    return { absoluteUrl, routerPath };
  }
}
