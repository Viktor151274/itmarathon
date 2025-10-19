import { Component, input, output } from '@angular/core';
import { CopyLink } from '../../../shared/components/copy-link/copy-link';
import { ButtonText, CaptionMessage, CopyLinkType } from '../../../app.enum';
import { Button } from '../../../shared/components/button/button';

@Component({
  selector: 'app-participant-info',
  imports: [CopyLink, Button],
  templateUrl: './participant-info.html',
  styleUrl: './participant-info.scss',
})
export class ParticipantInfo {
  public readonly firstName = input.required<string>();
  public readonly roomName = input.required<string>();
  public readonly link = input.required<string>();

  readonly buttonAction = output<void>();

  public readonly linkType = CopyLinkType.Light;
  public readonly caption = CaptionMessage.DoNotShare;
  public readonly buttonText = ButtonText.ViewInformation;

  public onButtonClick(): void {
    this.buttonAction.emit();
  }
}
