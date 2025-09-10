import { Injectable, signal, computed } from '@angular/core';
import { MessageType } from '../../app.enum';
import { ToasterStatus } from '../../app.enum';
import { MESSAGE_DURATION_MS } from '../../app.constants';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  readonly #message = signal('');
  readonly #type = signal<MessageType>(MessageType.Success);
  readonly #status = signal<ToasterStatus>(ToasterStatus.Hidden);

  #timeoutId: ReturnType<typeof setTimeout> | null = null;

  message = this.#message.asReadonly();
  type = this.#type.asReadonly();
  status = this.#status.asReadonly();

  readonly isVisible = computed(() => this.#status() === ToasterStatus.Visible);

  show(message: string, type: MessageType) {
    this.#message.set(message);
    this.#type.set(type);
    this.#status.set(ToasterStatus.Visible);

    if (this.#timeoutId) {
      clearTimeout(this.#timeoutId);
    }
    this.#timeoutId = setTimeout(() => this.hide(), MESSAGE_DURATION_MS);
  }

  hide() {
    this.#status.set(ToasterStatus.Hidden);
  }
}
