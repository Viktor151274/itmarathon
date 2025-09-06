import { Component, input, InputSignal } from '@angular/core';
import { InputLabel } from '../../../app.models';
import { InputSidebarText, InputPlaceholder, RegEx } from '../../../app.enum';
import { FormControl } from '@angular/forms';
import { Input } from '../input/input';
import { InputType } from '../../../app.enum';
@Component({
  selector: 'app-phone-input',
  imports: [Input],
  templateUrl: './phone-input.html',
  styleUrl: './phone-input.scss',
})
export class PhoneInput {
  control: InputSignal<FormControl> = input.required<FormControl>();
  type = InputType.Tel;
  label = input.required<InputLabel>();
  isRequired = input<boolean>(false);
  inputSidebarText: InputSidebarText = InputSidebarText.PhoneCodeUA;
  placeholder = input<InputPlaceholder>(InputPlaceholder.PhoneNumber);
  sidebarId = crypto.randomUUID();

  onInput(event: Event): void {
    this.#restrictInput(event);
  }

  #restrictInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    const pattern = new RegExp(RegEx.Digits, 'g');
    const digits = input.value.match(pattern);
    input.value = digits ? digits.join('') : '';
    if (input.value.length > 9) {
      input.value = input.value.slice(0, 9);
    }
  }
}
