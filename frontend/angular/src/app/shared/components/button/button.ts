import { Component, input } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.html',
  styleUrls: ['./button.scss'],
})
export class Button {
  readonly text = input<string>('');
  readonly iconName = input<string>('');
  readonly isIconRight = input<boolean>(false);
  readonly isDisabled = input<boolean>(false);

  readonly spritePath = 'assets/icons/sprite.svg';
}
