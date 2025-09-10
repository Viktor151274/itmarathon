import { Component, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { InputType } from '../../../app.enum';
import { Label } from '../label/label';
import { InputPlaceholder } from '../../../app.enum';
import type { InputLabel } from '../../../app.models';

@Component({
  selector: 'app-input',
  imports: [ReactiveFormsModule, Label],
  templateUrl: './input.html',
  styleUrl: './input.scss',
})
export class Input {
  control = input.required<FormControl>();
  label = input.required<InputLabel>();

  type = input<InputType>(InputType.Text);
  placeholder = input<InputPlaceholder | ''>('');
  isRequiredField = input<boolean>(false);
  maxLength = input<number | null>(null);
  ariaDescribedBy = input<string>();
}
