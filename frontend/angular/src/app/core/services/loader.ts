import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  readonly #visible = signal<boolean>(false);
  visible = this.#visible.asReadonly();

  show(): void {
    this.#visible.set(true);
  }

  hide(): void {
    this.#visible.set(false);
  }
}
