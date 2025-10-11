import { computed, Directive, inject, input, output } from '@angular/core';

import { AriaLabel, IconName } from '../../app.enum';
import { IMAGES_SPRITE_PATH } from '../../app.constants';
import { ModalService } from '../services/modal';
import type { ParentModalLayoutType } from '../../app.models';

@Directive()
export class ParentModalLayout<
  T extends ParentModalLayoutType = ParentModalLayoutType,
> {
  readonly headerPictureName = input.required<T['headerPictureName']>();
  readonly headerTitle = input.required<T['headerTitle']>();
  readonly buttonText = input.required<T['buttonText']>();

  readonly #modalService = inject(ModalService);

  readonly isModalOpen = this.#modalService.isModalOpen;

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
