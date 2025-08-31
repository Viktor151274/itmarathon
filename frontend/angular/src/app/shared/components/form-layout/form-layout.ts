import { Component, computed, input, output, signal } from '@angular/core';

import { IMAGES_SPRITE_PATH } from '../../../app.constants';
import { Button } from '../button/button';
import {
  ButtonText,
  FormSubtitle,
  FormTitle,
  IconName,
  PictureName,
} from '../../../app.enum';

@Component({
  selector: 'app-form-layout',
  templateUrl: './form-layout.html',
  styleUrl: './form-layout.scss',
  imports: [Button],
})
export class FormLayout {
  readonly title = input.required<FormTitle>();
  readonly subtitle = input.required<FormSubtitle>();
  readonly formPictureName = input.required<PictureName>();

  readonly budget = input<number>(0);
  readonly isFormValid = input<boolean>(false);

  readonly nextStep = output<void>();
  readonly previousStep = output<void>();
  readonly formCompleted = output<void>();

  public readonly formPicturePositionClass = computed(
    () => `form__picture--${this.formPictureName()}`
  );
  public readonly formPictureHref = computed(
    () => `${IMAGES_SPRITE_PATH}#${this.formPictureName()}`
  );

  public readonly buttonIconName = IconName.ArrowLeft;
  public readonly backButtonText = ButtonText.BackToPrevStep;
  public readonly completeButtonText = ButtonText.Complete;
  public readonly continueButtonText = ButtonText.Continue;

  // TODO: change signals isFirstStep and isLastStep once stepper service is done
  isFirstStep = signal(false);
  isLastStep = signal(false);

  public onNextStep(): void {
    this.nextStep.emit();
  }

  public onPreviousStep(): void {
    this.previousStep.emit();
  }

  public onComplete(): void {
    this.formCompleted.emit();
  }
}
