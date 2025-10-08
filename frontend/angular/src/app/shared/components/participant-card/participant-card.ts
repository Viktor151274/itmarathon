import {
  Component,
  computed,
  ElementRef,
  HostBinding,
  inject,
  input,
} from '@angular/core';
import { IconButton } from '../icon-button/icon-button';
import {
  AriaLabel,
  IconName,
  MessageType,
  PersonalLink,
  PopupPosition,
} from '../../../app.enum';
import { PopupService } from '../../../core/services/popup';
import { copyToClipboard } from '../../../utils/copy';
import { User } from '../../../app.models';
import { UrlService } from '../../../core/services/url';

@Component({
  selector: 'li[app-participant-card]',
  imports: [IconButton],
  templateUrl: './participant-card.html',
  styleUrl: './participant-card.scss',
})
export class ParticipantCard {
  readonly #popup = inject(PopupService);
  private readonly urlService = inject(UrlService);
  private readonly host = inject(ElementRef<HTMLElement>);
  public readonly participant = input.required<User>();
  public readonly showCopyIcon = input<boolean>(false);
  public readonly userCode = input<string>('');
  public readonly showInfoIcon = input<boolean>(false);

  readonly iconCopy = IconName.Link;
  readonly ariaLabelCopy = AriaLabel.ParticipantLink;
  readonly iconInfo = IconName.Info;
  readonly ariaLabelInfo = AriaLabel.Info;

  @HostBinding('tabindex') tab = 0;
  @HostBinding('class.list-row') rowClass = true;

  isAdmin = () => this.participant().isAdmin;
  readonly isCurrentUser = computed(() => {
    const code = this.userCode();
    return !!code && this.participant()?.userCode === code;
  });

  fullName = () =>
    `${this.participant().firstName} ${this.participant().lastName}`;

  async copyRoomLink(): Promise<void> {
    const host = this.host.nativeElement;
    const code = this.participant().userCode;

    if (!code) {
      this.#popup.show(
        host,
        PopupPosition.Right,
        { message: PersonalLink.Error, type: MessageType.Error },
        false
      );
      return;
    }

    const { absoluteUrl } = this.urlService.getNavigationLinks(code, 'room');
    const ok = await copyToClipboard(absoluteUrl);

    this.#popup.show(
      host,
      PopupPosition.Right,
      {
        message: ok ? PersonalLink.Success : PersonalLink.Error,
        type: ok ? MessageType.Success : MessageType.Error,
      },
      false
    );
  }

  onInfoClick(): void {
    const participant = this.participant();
    if (!this.participant().isAdmin) return;

    const container = this.host.nativeElement.closest(
      'app-participant-list'
    ) as HTMLElement;
    const message = participant.email
      ? `${participant.phone}
         ${participant.email}`
      : `${participant.phone}`;

    this.#popup.show(
      container,
      PopupPosition.Right,
      {
        message: message,
        type: MessageType.Info,
      },
      true
    );
  }

  onCopyHover(target: EventTarget | null): void {
    if (target instanceof HTMLElement) {
      this.#popup.show(
        target,
        PopupPosition.Right,
        { message: PersonalLink.Info, type: MessageType.Info },
        true
      );
    }
  }

  onCopyLeave(target: EventTarget | null): void {
    if (target instanceof HTMLElement) {
      this.#popup.hide(target);
    }
  }
}
