import {
  Component,
  computed,
  HostListener,
  input,
  output,
} from '@angular/core';
import { animate, style, transition, trigger } from '@angular/animations';

import { IMAGES_SPRITE_PATH } from '../../../../app.constants';
import { Button } from '../../button/button';
import { IconButton } from '../../icon-button/icon-button';
import {
  AriaLabel,
  ButtonText,
  IconName,
  ModalTitle,
  PictureName,
} from '../../../../app.enum';
import { FocusTrap } from '../../../../core/directives/focus-trap';

@Component({
  selector: 'app-modal-layout-with-header',
  imports: [Button, IconButton, FocusTrap],
  templateUrl: './modal-layout-with-header.html',
  styleUrl: './modal-layout-with-header.scss',
  animations: [
    trigger('dissolve', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('500ms ease-out', style({ opacity: 1 })),
      ]),
      transition(':leave', [
        style({ opacity: 1 }),
        animate('500ms ease-in', style({ opacity: 0 })),
      ]),
    ]),
  ],
})
export class ModalLayoutWithHeader {
  readonly headerPictureName = input.required<PictureName>();
  readonly headerTitle = input.required<ModalTitle>();
  readonly buttonText = input.required<ButtonText>();

  readonly isModalOpen = input<boolean>(false);

  readonly closeModal = output<void>();
  readonly buttonAction = output<void>();

  public readonly headerPictureHref = computed(
    () => `${IMAGES_SPRITE_PATH}#${this.headerPictureName()}`
  );

  public readonly closeIcon = IconName.Close;
  public readonly closeButtonAriaLabel = AriaLabel.Close;

  public onCloseModal(): void {
    this.closeModal.emit();
  }

  public onActionButtonClick(): void {
    this.buttonAction.emit();
  }
}
