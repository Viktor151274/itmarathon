import {
  Component,
  ElementRef,
  ViewChild,
  AfterViewInit,
  computed,
  input,
  signal,
  inject,
  Injector,
  effect,
  OnInit,
} from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { toSignal } from '@angular/core/rxjs-interop';

import { IconButton } from '../icon-button/icon-button';
import {
  AriaLabel,
  IconName,
  InvitationNotePopup,
  ItemPosition,
  MessageType,
  PopupPosition,
} from '../../../app.enum';
import { copyToClipboard } from '../../../utils/copy';
import { PopupService } from '../../../core/services/popup';
import { CharCounter } from '../../../core/directives/char-counter';

@Component({
  selector: 'app-invitation-note',
  standalone: true,
  imports: [IconButton, ReactiveFormsModule, CharCounter],
  templateUrl: './invitation-note.html',
  styleUrl: './invitation-note.scss',
})
export class InvitationNote implements AfterViewInit, OnInit {
  invitationNote = input.required<string>();
  invitationLink = input.required<string>();
  maxLength = input<number | null>(null);
  editable = input<boolean>(true);
  control = new FormControl('');

  @ViewChild('popupHost', { read: ElementRef })
  popupHost?: ElementRef<HTMLElement>;
  @ViewChild('textarea') taRef!: ElementRef<HTMLTextAreaElement>;

  readonly #popup = inject(PopupService);
  readonly #injector = inject(Injector);

  readonly #isEdit = signal(false);
  readonly isEdit = computed(() => this.#isEdit());

  readonly ariaLabelSave = AriaLabel.SaveButton;
  readonly ariaLabelCopy = AriaLabel.CopyButton;
  readonly ariaLabelEdit = AriaLabel.EditButton;
  readonly iconSave = IconName.Save;
  readonly iconCopy = IconName.Copy;
  readonly iconEdit = IconName.Edit;
  readonly charCounterPositionY = ItemPosition.Below;

  readonly controlValue = toSignal(this.control.valueChanges, {
    initialValue: this.control.value,
    injector: this.#injector,
  });

  ngOnInit(): void {
    effect(
      () => {
        this.control.setValue(this.invitationNote(), { emitEvent: false });
      },
      { injector: this.#injector }
    );
  }

  ngAfterViewInit(): void {
    this.control.setValue(this.invitationNote());
    effect(
      () => {
        this.controlValue();
        this.#resizeTextarea();
      },
      { injector: this.#injector }
    );
  }

  #resizeTextarea(): void {
    const el = this.taRef.nativeElement;
    if (!el) return;

    el.style.height = 'auto';
    el.style.height = el.scrollHeight + 'px';
  }

  toggleEdit(): void {
    if (!this.editable()) return;
    this.#isEdit.set(!this.#isEdit());

    if (this.#isEdit()) {
      this.taRef.nativeElement.focus();
    }
  }

  readonly copyPayload = computed(() => {
    const text = (this.controlValue() ?? '').trimEnd();
    const link = (this.invitationLink() ?? '').toString().trim();
    const joined = link ? `${text}\n\n${link}` : text;
    const limit = this.maxLength() ?? 1000;
    return joined.slice(0, limit);
  });

  async copy(): Promise<void> {
    const ok = await copyToClipboard(this.copyPayload());
    const host = this.popupHost?.nativeElement;
    if (!host) return;

    this.#popup.show(
      host,
      PopupPosition.Right,
      {
        message: ok ? InvitationNotePopup.Success : InvitationNotePopup.Error,
        type: ok ? MessageType.Success : MessageType.Error,
      },
      false
    );
  }
}
