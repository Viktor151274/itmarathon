import { Component, inject, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  NonNullableFormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';

import { Stepper } from '../shared/components/stepper/stepper';
import { CreateRoomForm } from '../components/forms/create-room-form/create-room-form';
import { StepperManager } from '../core/services/stepper-manager';
import { STEPPER_LABELS_TOKEN } from '../core/services/tokens/stepper-labels.token';
import { CREATE_ROOM_STEPPER_LABELS } from '../app.constants';
import { AddYourDetailsForm } from '../components/forms/add-your-details-form/add-your-details-form';
import { AddYourWishesForm } from '../components/forms/add-your-wishes-form/add-your-wishes-form';
import { RadioButtonValue } from '../app.enum';
import type {
  BasicUserDetails,
  AddYourDetailsFormType,
  CreateRoomFormType,
  GiftIdeaFormType,
  WishListItem,
  RoomCreationRequest,
  SurpriseGiftFormType,
  BasicRoomDetails,
} from '../app.models';

@Component({
  selector: 'app-create-room',
  imports: [
    Stepper,
    CreateRoomForm,
    AddYourDetailsForm,
    AddYourWishesForm,
    ReactiveFormsModule,
  ],
  templateUrl: './create-room.html',
  styleUrl: './create-room.scss',
  providers: [
    {
      provide: STEPPER_LABELS_TOKEN,
      useValue: CREATE_ROOM_STEPPER_LABELS,
    },
    StepperManager,
  ],
})
export class CreateRoom implements OnInit {
  readonly #stepperManagerService = inject(StepperManager);
  readonly #formBuilder = inject(NonNullableFormBuilder);

  public readonly currentStep = this.#stepperManagerService.currentStep;
  public readonly stepsCount = this.#stepperManagerService.maxSteps;
  public readonly stepLabels = this.#stepperManagerService.labels;
  public readonly radioControl = new FormControl();

  public createRoomForm!: FormGroup<CreateRoomFormType>;
  public addYourDetailsForm!: FormGroup<AddYourDetailsFormType>;
  public giftIdeaForm!: FormGroup<GiftIdeaFormType>;
  public surpriseGiftForm!: FormGroup<SurpriseGiftFormType>;

  ngOnInit(): void {
    this.createRoomForm = this.#initCreateRoomForm();
    this.addYourDetailsForm = this.#initAddYourDetailsForm();
    this.giftIdeaForm = this.#initGiftIdeaForm();
    this.surpriseGiftForm = this.#initSurpriseGiftForm();
  }

  public onFormCompleted(): void {
    const combinedFormData: RoomCreationRequest = this.#combineFormData();
    // TODO: add implementation of sending data to the backend
  }

  #combineFormData(): RoomCreationRequest {
    const wantSurprise =
      this.radioControl.value === RadioButtonValue.SurpriseGift;
    const wishList = !wantSurprise
      ? (this.giftIdeaForm.value.wishList as WishListItem[])
      : [];
    const interests = wantSurprise
      ? (this.surpriseGiftForm.value.interests as string)
      : '';

    return {
      room: this.createRoomForm.value as BasicRoomDetails,
      adminUser: {
        ...(this.addYourDetailsForm.value as BasicUserDetails),
        wantSurprise,
        wishList,
        interests,
      },
    };
  }

  #initCreateRoomForm(): FormGroup<CreateRoomFormType> {
    return this.#formBuilder.group({
      name: [''],
      description: [''],
      giftExchangeDate: [''],
      giftMaximumBudget: [0],
    });
  }

  #initAddYourDetailsForm(): FormGroup<AddYourDetailsFormType> {
    return this.#formBuilder.group({
      firstName: [''],
      lastName: [''],
      phone: [''],
      email: [''],
      deliveryInfo: [''],
    });
  }

  #initGiftIdeaForm(): FormGroup<GiftIdeaFormType> {
    return this.#formBuilder.group({
      wishList: this.#formBuilder.array([
        this.#formBuilder.group({
          name: [''],
          infoLink: [''],
        }),
      ]),
    });
  }

  #initSurpriseGiftForm(): FormGroup<SurpriseGiftFormType> {
    return this.#formBuilder.group({
      interests: [''],
    });
  }
}
