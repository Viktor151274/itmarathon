import {
  BaseLabel,
  RadioButtonLabel,
  StepLabel,
  TextareaLabel,
} from './app.enum';

export interface StepperItem {
  isActive: boolean;
  isFilled: boolean;
  label: StepLabel;
}

export type InputLabel = BaseLabel | RadioButtonLabel | TextareaLabel;
