import { Component, computed, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { InputSidebarText, InputPlaceholder, RegEx } from '../../../app.enum';
import { Input } from '../input/input';
import { InputType } from '../../../app.enum';
import type { InputLabel } from '../../../app.models';
import { generateId } from '../../../utils/generate-id';

@Component({
  selector: 'app-phone-input',
  imports: [Input, ReactiveFormsModule],
  templateUrl: './phone-input.html',
  styleUrl: './phone-input.scss',
})
export class PhoneInput {
  readonly control = input.required<FormControl>();
  readonly label = input.required<InputLabel>();

  readonly isRequired = input<boolean>(false);
  readonly placeholder = input<InputPlaceholder>(InputPlaceholder.PhoneNumber);

  public readonly inputSidebarText = InputSidebarText.PhoneCodeUA;
  public readonly type = InputType.Tel;
  public readonly sidebarId = generateId();

  public onInput(event: Event): void {
    this.#restrictInput(event);
  }

  public onBlur(): void {
    this.control().markAsTouched();
  }

  readonly showError = computed(() => {
    const c = this.control();
    return (c.touched || c.dirty) && c.invalid;
  });

  #restrictInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    const onlyDigits =
      (input.value ?? '').match(new RegExp(RegEx.Digits, 'g'))?.join('') ?? '';
    const digits = onlyDigits.slice(0, 9);
    input.value = digits;
    this.control().setValue(digits, { emitEvent: false });
  }
}
