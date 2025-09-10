import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

import { FormLayout } from '../../../shared/components/form-layout/form-layout';
import { Textarea } from '../../../shared/components/textarea/textarea';
import { DatePicker } from '../../../shared/components/date-picker/date-picker';
import { Input } from '../../../shared/components/input/input';
import {
  BaseLabel,
  FormSubtitle,
  FormTitle,
  InputPlaceholder,
  PictureName,
  TextareaLabel,
  TextareaPlaceholder,
} from '../../../app.enum';
import { ParentForm } from '../../../core/directives/parent-form';
import { BudgetInput } from '../../../shared/components/budget-input/budget-input';
import { CharCounter } from '../../../core/directives/char-counter';
import type { CreateRoomFormType } from '../../../app.models';

@Component({
  selector: 'app-create-room-form',
  imports: [FormLayout, Input, Textarea, DatePicker, BudgetInput, CharCounter],
  templateUrl: './create-room-form.html',
  styleUrl: './create-room-form.scss',
})
export class CreateRoomForm extends ParentForm implements OnInit {
  public form!: FormGroup<CreateRoomFormType>;

  ngOnInit(): void {
    this.form = this.#initCreateRoomForm();
  }

  public get roomNameControl(): FormControl {
    return this.form.get('roomName') as FormControl;
  }

  public get roomDescriptionControl(): FormControl {
    return this.form.get('roomDescription') as FormControl;
  }

  public get exchangeDateControl(): FormControl {
    return this.form.get('exchangeDate') as FormControl;
  }

  public get giftBudgetControl(): FormControl {
    return this.form.get('giftBudget') as FormControl;
  }

  public readonly formTitle = FormTitle.CreateRoom;
  public readonly formSubtitle = FormSubtitle.CreateRoom;
  public readonly formPictureName = PictureName.Star;

  public readonly roomName = BaseLabel.RoomName;
  public readonly exchangeDate = BaseLabel.ExchangeDate;
  public readonly giftBudget = BaseLabel.Budget;
  public readonly roomDescription = TextareaLabel.RoomDescription;

  public readonly roomNamePlaceholder = InputPlaceholder.EnterName;
  public readonly exchangeDatePlaceholder = InputPlaceholder.Date;
  public readonly giftBudgetPlaceholder = InputPlaceholder.Budget;
  public readonly roomDescriptionPlaceholder = TextareaPlaceholder.EnterMessage;

  public readonly textareaMaxLength = 200;

  #initCreateRoomForm(): FormGroup<CreateRoomFormType> {
    return this.formBuilder.group({
      roomName: [''],
      roomDescription: [''],
      exchangeDate: [''],
      giftBudget: [''],
    });
  }
}
