import { inject, Injectable } from '@angular/core';
import { Router, UrlTree } from '@angular/router';

import { ErrorMessage, MessageType, Path } from '../../app.enum';
import { ToastService } from './toast';

@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  readonly #router = inject(Router);
  readonly #toaster = inject(ToastService);

  public redirectWithToast(
    path: Path,
    errorMessage: ErrorMessage,
    type: MessageType
  ): UrlTree {
    this.#toaster.show(errorMessage, type);
    return this.#router.createUrlTree([`/${path}`]);
  }
}
