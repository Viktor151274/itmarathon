import { StepLabel } from './app.enum';

export interface StepperItem {
  isActive: boolean;
  isFilled: boolean;
  label: StepLabel;
}
