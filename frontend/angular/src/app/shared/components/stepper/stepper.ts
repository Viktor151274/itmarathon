import { Component, computed, input } from '@angular/core';

import { ICONS_SPRITE_PATH } from '../../../app.constants';
import { IconName } from '../../../app.enum';
import type { StepperItem } from '../../../app.models';

@Component({
  selector: 'app-stepper',
  templateUrl: './stepper.html',
  styleUrl: './stepper.scss',
})
export class Stepper {
  stepperData = input.required<StepperItem[]>();
  successIconHref = `${ICONS_SPRITE_PATH}#${IconName.SuccessMark}`;

  stepsCount = computed(() => this.stepperData().length);
}
