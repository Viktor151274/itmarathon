import { Component, inject } from '@angular/core';

import { Stepper } from '../shared/components/stepper/stepper';
import { CreateRoomForm } from '../components/forms/create-room-form/create-room-form';
import { StepperManager } from '../core/services/stepper-manager';
import { STEPPER_LABELS_TOKEN } from '../core/services/tokens/stepper-labels.token';
import { CREATE_ROOM_STEPPER_LABELS } from '../app.constants';
import { AddYourDetailsForm } from '../components/forms/add-your-details-form/add-your-details-form';

@Component({
  selector: 'app-create-room',
  imports: [Stepper, CreateRoomForm, AddYourDetailsForm],
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
export class CreateRoom {
  readonly #stepperManagerService = inject(StepperManager);

  public readonly currentStep = this.#stepperManagerService.currentStep;
  public readonly stepsCount = this.#stepperManagerService.maxSteps;
  public readonly stepLabels = this.#stepperManagerService.labels;
}
