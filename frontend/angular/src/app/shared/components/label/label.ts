import { Component, input } from '@angular/core';

import { RadioButtonLabel } from '../../../app.enum';

@Component({
  selector: 'app-label',
  templateUrl: './label.html',
  styleUrl: './label.scss',
})
export class Label {
  readonly label = input.required<RadioButtonLabel>();

  readonly isLabelRadio = input<boolean>(false);
  readonly isRequiredField = input<boolean>(false);
}
