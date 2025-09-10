import { Injectable } from '@angular/core';
import { FormControl, ValidationErrors } from '@angular/forms';

import { ErrorMessage } from '../../app.enum';

@Injectable({
  providedIn: 'root',
})
export class FormValidation {
  public getErrorMessage(control: FormControl): ErrorMessage {
    const errors: ValidationErrors = control.errors || {};

    return (
      ErrorMessage[Object.keys(errors)[0] as keyof typeof ErrorMessage]
      || ErrorMessage.DefaultMessage
    );
  }

  public hasError(control: FormControl): boolean {
    return Boolean(control.invalid && control.touched);
  }
}
