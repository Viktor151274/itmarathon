import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ModalService {
  readonly #isModalOpen = signal<boolean>(false);

  public readonly isModalOpen = this.#isModalOpen.asReadonly();

  public toggleModal(): void {
    this.#isModalOpen.update((prev) => !prev);
  }
}
