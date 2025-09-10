import { Directive, inject } from '@angular/core';
import { NonNullableFormBuilder } from '@angular/forms';

import { ItemPosition } from '../../app.enum';

@Directive({
  selector: '[appParentForm]',
})
export class ParentForm {
  public readonly formBuilder = inject(NonNullableFormBuilder);

  public readonly inputMaxLength = 40;
  public readonly inputCharCounterPositionY = ItemPosition.Center;
}
