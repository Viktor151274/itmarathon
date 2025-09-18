import { inject, Injectable } from '@angular/core';
import { ErrorMessage, MessageType, Path } from '../../app.enum';
import { Router, UrlTree } from '@angular/router';
import { ToastService } from './toast';

@Injectable({
  providedIn: 'root',
})
export class RedirectWithToastService {
  #router = inject(Router);
  #toaster = inject(ToastService);

  public redirect(
    path: Path,
    errorMessage: ErrorMessage,
    type: MessageType
  ): UrlTree {
    this.#toaster.show(errorMessage, type);
    return this.#router.createUrlTree([`/${path}`]);
  }
}
