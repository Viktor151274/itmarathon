import { Component, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { InputType } from '../../../app.enum';
import { Label } from '../label/label';
import { InputLabel } from '../../../app.models';
import { InputPlaceholder } from '../../../app.enum';

@Component({
  selector: 'app-input',
  imports: [ReactiveFormsModule, Label],
  templateUrl: './input.html',
  styleUrl: './input.scss',
})
export class Input {
  type = input<InputType>(InputType.Text);
  control = input.required<FormControl>();
  label = input.required<InputLabel>();
  placeholder = input<InputPlaceholder | ''>('');
  isRequiredField = input<boolean>(false);
  maxLength = input<number | null>(null);
  ariaDescribedBy = input<string>();
}
