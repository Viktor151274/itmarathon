import { Component, computed, inject, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { CaptionMessage } from '../../../app.enum';
import { FormValidation } from '../../../core/services/form-validation';
import type { FieldHintMessage } from '../../../app.models';

@Component({
  selector: 'app-field-hint',
  imports: [ReactiveFormsModule],
  templateUrl: './field-hint.html',
  styleUrl: './field-hint.scss',
})
export class FieldHint {
  readonly control = input.required<FormControl>();
  readonly captionMessage = input<CaptionMessage>(CaptionMessage.EmptyMessage);

  public readonly hintMessage = computed(() => this.#getHintMessage());
  public readonly hasError = computed(() =>
    this.#formValidationService.hasError(this.control())
  );

  readonly #formValidationService = inject(FormValidation);

  #getHintMessage(): FieldHintMessage {
    return this.hasError()
      ? this.#formValidationService.getErrorMessage(this.control())
      : this.captionMessage();
  }
}
