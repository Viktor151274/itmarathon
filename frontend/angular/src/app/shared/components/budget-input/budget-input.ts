import { Component, input, InputSignal } from '@angular/core';
import { InputLabel } from '../../../app.models';
import {
  InputType,
  InputPlaceholder,
  InputSidebarText,
  RegEx,
} from '../../../app.enum';
import { FormControl } from '@angular/forms';
import { Input } from '../input/input';

@Component({
  selector: 'app-budget-input',
  imports: [Input],
  templateUrl: './budget-input.html',
  styleUrl: './budget-input.scss',
})
export class BudgetInput {
  control: InputSignal<FormControl> = input.required<FormControl>();
  type = InputType.Number;
  label = input.required<InputLabel>();
  inputSidebarText: InputSidebarText = InputSidebarText.CurrencyUAH;
  isRequired = input<boolean>(false);
  placeholder = input<InputPlaceholder>(InputPlaceholder.Budget);
  sidebarId = crypto.randomUUID();

  onInput(event: Event): void {
    this.#restrictInput(event);
  }
  #restrictInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    const pattern = new RegExp(RegEx.Digits, 'g');
    const digits = input.value.match(pattern);
    input.value = digits ? digits.join('') : '';
  }
}
