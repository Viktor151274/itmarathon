import { Component, computed, inject, input } from '@angular/core';
import { RoomDataCard } from '../../../shared/components/room-data-card/room-data-card';
import {
  ButtonText,
  InvitationNotePopup,
  MessageType,
  PictureName,
  PopupPosition,
  RoomDataCardVariant,
  RoomInfoCardTitle,
} from '../../../app.enum';
import { DatePipe } from '@angular/common';
import { Button } from '../../../shared/components/button/button';
import { PopupService } from '../../../core/services/popup';
import { copyToClipboard } from '../../../utils/copy';
import { BudgetPipe } from '../../../shared/pipes/budget.pipe';

@Component({
  selector: 'app-room-info',
  imports: [RoomDataCard, DatePipe, Button, BudgetPipe],
  templateUrl: './room-info.html',
  styleUrl: './room-info.scss',
})
export class RoomInfo {
  readonly title = input.required<string>();
  readonly description = input.required<string>();
  readonly exchangeDateString = input.required<string>();
  readonly budget = input.required<number>();
  readonly invitationNote = input.required<string>();
  readonly invitationLink = input.required<string>();
  readonly isAdmin = input<boolean>(false);
  readonly isDrawn = input<boolean>(false);

  private readonly popupService = inject(PopupService);

  private readonly popupPositionRight = PopupPosition.Right;
  private readonly successMessageType = MessageType.Success;
  private readonly errorMessageType = MessageType.Error;

  public readonly exchangeDate = computed(
    () => new Date(this.exchangeDateString())
  );
  public readonly noteWithLink = computed(
    () => `${this.invitationNote()}\n\n${this.invitationLink()}`
  );
  public readonly variant = RoomDataCardVariant.Light;
  public readonly presentsIcon = PictureName.BigPresents;
  public readonly starIcon = PictureName.Star2;
  public readonly letterIcon = PictureName.Letter;
  public readonly dataTitle = RoomInfoCardTitle.ExchangeDate;
  public readonly budgetTitle = RoomInfoCardTitle.GiftBudget;
  public readonly invitationTitle = RoomInfoCardTitle.InvitationNote;
  public readonly inviteButtonText = ButtonText.InviteNewMembers;

  public async onClick(event: MouseEvent) {
    const button = event.currentTarget as HTMLElement;
    const isCopied = await copyToClipboard(this.noteWithLink());
    if (isCopied) {
      this.popupService.show(button, this.popupPositionRight, {
        message: InvitationNotePopup.Success,
        type: this.successMessageType,
      });
    } else {
      this.popupService.show(button, this.popupPositionRight, {
        message: InvitationNotePopup.Error,
        type: this.errorMessageType,
      });
    }
  }
}
