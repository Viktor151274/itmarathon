import { ComponentRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { FormControl } from '@angular/forms';
import {
  BaseLabel,
  CaptionMessage,
  ErrorMessage,
  RadioButtonLabel,
  StepLabel,
  TextareaLabel,
  MessageType,
} from './app.enum';
import { Message } from './shared/components/message/message';

export interface StepperItem {
  isActive: boolean;
  isFilled: boolean;
  label: StepLabel;
}

export type InputLabel = BaseLabel | RadioButtonLabel | TextareaLabel;

export type FieldHintMessage = CaptionMessage | ErrorMessage;

export interface MessageOptions {
  message: string;
  type: MessageType;
}

export interface PopupInstance {
  ref?: ComponentRef<Message> | null;
  subscription?: Subscription;
  timerId?: ReturnType<typeof setTimeout>;
}

export type StyleMap = Record<string, string>;

export interface CreateRoomFormType {
  roomName: FormControl<string>;
  roomDescription: FormControl<string>;
  exchangeDate: FormControl<string>;
  giftBudget: FormControl<string>;
}
