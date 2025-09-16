import { Component, inject } from '@angular/core';
import { PageLayout } from '../shared/components/page-layout/page-layout';
import {
  ButtonText,
  ButtonType,
  CaptionMessage,
  PageSubtitle,
  PageTitle,
  PictureName,
} from '../app.enum';
import { CopyLink } from '../shared/components/copy-link/copy-link';
import { InvitationNote } from '../shared/components/invitation-note/invitation-note';
import { Router } from '@angular/router';
import { UrlService } from '../core/services/url';

@Component({
  selector: 'app-success',
  imports: [PageLayout, CopyLink, InvitationNote],
  templateUrl: './success.html',
  styleUrl: './success.scss',
})
export class Success {
  readonly #router = inject(Router);
  private readonly urlService = inject(UrlService);

  // TODO: add actually data from backend
  readonly userCode = 'yourlink456';
  readonly roomCode = 'abc123';

  private readonly pathsRoom = this.urlService.build(this.roomCode, 'room');
  private readonly pathsPersonalLink = this.urlService.build(this.userCode);

  readonly personalLink = this.pathsPersonalLink.absoluteUrl;
  readonly roomLink = this.pathsRoom.absoluteUrl;
  readonly routerPath = this.pathsPersonalLink.routerPath;

  readonly btnText = ButtonText.Success;
  readonly btnType = ButtonType.Button;

  readonly doNotShare = CaptionMessage.DontShare;

  // TODO: add actually data from backend
  readonly invitationNoteDefault = `Hey!

Join our Secret Nick and make this holiday season magical! 🎄
You'll get to surprise someone with a gift — and receive one too. 🎅✨

Let the holiday fun begin! 🌟

🎁 Join here:`;

  public readonly pageTitle = PageTitle.Success;
  public readonly pageSubtitle = PageSubtitle.Success;
  public readonly pagePictureName = PictureName.Flat;

  public readonly invitationNoteMaxLength = 1000;

  public onButtonClick(): void {
    void this.#router.navigateByUrl(this.routerPath);
  }
}
