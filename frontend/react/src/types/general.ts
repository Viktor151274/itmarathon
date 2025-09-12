import type { ChangeEvent } from "react";

export type InputChangeEvent = ChangeEvent<
  HTMLInputElement | HTMLTextAreaElement
>;

export type InputChangeHandler = (e: InputChangeEvent) => void;
