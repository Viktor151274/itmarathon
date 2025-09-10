import { Directive, inject } from '@angular/core';
import { NonNullableFormBuilder } from '@angular/forms';

@Directive({
  selector: '[appParentForm]',
})
export class ParentForm {
  public readonly formBuilder = inject(NonNullableFormBuilder);

  public readonly inputMaxLength = 40;
  public readonly textareaMaxLength = 200;
}
