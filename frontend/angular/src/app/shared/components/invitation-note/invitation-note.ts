import {
  Component,
  ElementRef,
  ViewChild,
  AfterViewInit,
  computed,
  input,
  signal,
  inject,
} from '@angular/core';

import { IconButton } from '../icon-button/icon-button';
import {
  AriaLabel,
  IconName,
  MessageType,
  PopupPosition,
} from '../../../app.enum';
import { copyToClipboard } from '../../../utils/copy';
import { PopupService } from '../../../core/services/popup';
import { InvitationNotePopup } from '../../../app.enum';

@Component({
  selector: 'app-invitation-note',
  standalone: true,
  imports: [IconButton],
  templateUrl: './invitation-note.html',
  styleUrl: './invitation-note.scss',
})
export class InvitationNote implements AfterViewInit {
  rawNote = input.required<string>();

  maxLength = input<number | null>(null);
  editable = input<boolean>(false);
  invitationLink = input<string>('');

  @ViewChild('popupHost', { read: ElementRef })
  popupHost?: ElementRef<HTMLElement>;
  @ViewChild('textarea') taRef?: ElementRef<HTMLTextAreaElement>;

  #popup = inject(PopupService);

  readonly #isEdit = signal(false);
  readonly isEdit = computed(() => this.#isEdit());
  readonly #text = signal<string>('');

  ariaLabelSave = AriaLabel.SaveButton;
  ariaLabelCopy = AriaLabel.CopyButton;
  ariaLabelEdit = AriaLabel.EditButton;

  iconSave = IconName.Save;
  iconCopy = IconName.Copy;
  iconEdit = IconName.Edit;

  protected readonly parsed = computed(() => {
    const note = (this.rawNote() ?? '').toString();
    const link = (this.invitationLink() ?? '').toString().trim();

    if (!link) {
      return { text: note, linkUrl: '' };
    }

    const lastIdx = note.lastIndexOf(link);
    if (lastIdx === -1) {
      return { text: note, linkUrl: link };
    }

    const before = note.slice(0, lastIdx).replace(/\s+$/, '');

    return { text: before, linkUrl: link };
  });

  readonly textPart = computed(() => this.parsed().text);

  readonly textareaValue = computed(() =>
    this.isEdit() ? this.#text() : this.textPart()
  );

  ngAfterViewInit(): void {
    this.#setInitialHeight();
  }

  #setInitialHeight(): void {
    const el = this.taRef?.nativeElement;
    if (!el) return;

    el.style.height = 'auto';
    el.style.height = el.scrollHeight + 'px';
  }

  toggleEdit(): void {
    if (!this.editable()) return;
    const willEdit = !this.#isEdit();
    if (willEdit) {
      this.#text.set(this.textPart());
    }
    this.#isEdit.set(willEdit);
  }

  onInput(val: string): void {
    const limit = this.maxLength() ?? 1000;
    this.#text.set((val ?? '').slice(0, limit));
  }

  readonly copyPayload = computed(() => {
    const text = this.textareaValue().trimEnd();
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
        message: ok ? InvitationNotePopup.success : InvitationNotePopup.error,
        type: ok ? MessageType.Success : MessageType.Error,
      },
      false
    );
  }
}
