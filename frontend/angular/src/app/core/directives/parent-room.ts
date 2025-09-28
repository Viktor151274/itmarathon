import { Directive, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, NonNullableFormBuilder, Validators } from '@angular/forms';

import { StepperManager } from '../services/stepper-manager';
import { RadioButtonValue } from '../../app.enum';
import type {
  AddYourDetailsFormType,
  BasicUserDetails,
  GiftIdeaFormType,
  SurpriseGiftFormType,
  UserDetails,
  WishListItem,
} from '../../app.models';
import { phoneValidator } from '../../shared/validators/phone.validator';

@Directive()
export class ParentRoom implements OnInit {
  public readonly formBuilder = inject(NonNullableFormBuilder);
  readonly #stepperManagerService = inject(StepperManager);

  public readonly currentStep = this.#stepperManagerService.currentStep;
  public readonly stepsCount = this.#stepperManagerService.maxSteps;
  public readonly stepLabels = this.#stepperManagerService.labels;

  public readonly radioControl = new FormControl();

  public addYourDetailsForm!: FormGroup<AddYourDetailsFormType>;
  public giftIdeaForm!: FormGroup<GiftIdeaFormType>;
  public surpriseGiftForm!: FormGroup<SurpriseGiftFormType>;

  ngOnInit(): void {
    this.addYourDetailsForm = this.initAddYourDetailsForm();
    this.giftIdeaForm = this.initGiftIdeaForm();
    this.surpriseGiftForm = this.initSurpriseGiftForm();
  }

  public getUserDetails(): UserDetails {
    const wantSurprise =
      this.radioControl.value === RadioButtonValue.SurpriseGift;
    const wishList = !wantSurprise
      ? (this.giftIdeaForm.value.wishList as WishListItem[])
      : [];
    const interests = wantSurprise
      ? (this.surpriseGiftForm.value.interests as string)
      : '';

    return {
      ...(this.addYourDetailsForm.value as BasicUserDetails),
      wantSurprise,
      wishList,
      interests,
    };
  }

  public initAddYourDetailsForm(): FormGroup<AddYourDetailsFormType> {
    return this.formBuilder.group({
      firstName: [''],
      lastName: [''],
      phone: [''],
      email: [''],
      deliveryInfo: [''],
    });
  }

  public initGiftIdeaForm(): FormGroup<GiftIdeaFormType> {
    return this.formBuilder.group({
      wishList: this.formBuilder.array([
        this.formBuilder.group({
          name: [''],
          infoLink: [''],
        }),
      ]),
    });
  }

  public initSurpriseGiftForm(): FormGroup<SurpriseGiftFormType> {
    return this.formBuilder.group({
      interests: [''],
    });
  }
}
