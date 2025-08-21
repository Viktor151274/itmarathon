import { StepLabel } from './app.enum';
import type { StepperItem } from './app.models';

export const ICONS_SPRITE_PATH = '/assets/icons/icons-sprite.svg';

export const IMAGES_SPRITE_PATH = '/assets/images/images-sprite.svg';

export const STEPPER_DEFAULT_DATA_CREATE_ROOM: StepperItem[] = [
  { isActive: true, isFilled: false, label: StepLabel.CreateRoom },
  { isActive: false, isFilled: false, label: StepLabel.AddParticipantInfo },
  { isActive: false, isFilled: false, label: StepLabel.AddWishlist },
];

export const STEPPER_DEFAULT_DATA_JOIN_ROOM: StepperItem[] = [
  { isActive: true, isFilled: false, label: StepLabel.AddParticipantInfo },
  { isActive: false, isFilled: false, label: StepLabel.AddWishlist },
];

export const PRIVACY_POLICY_PATH = '/assets/pdfs/privacy-policy.pdf';
export const PRIVACY_NOTICE_PATH = '/assets/pdfs/privacy-notice.pdf';
