import { Component, input } from '@angular/core';
import { Input } from '../input/input';
import { InputPlaceholder, ItemPosition } from '../../../app.enum';
import { BaseLabel } from '../../../app.enum';
import { FormControl } from '@angular/forms';
import { CharCounter } from '../../../core/directives/char-counter';

@Component({
  selector: 'app-gift-idea',
  imports: [Input, CharCounter],
  templateUrl: './gift-idea.html',
  styleUrl: './gift-idea.scss',
})
export class GiftIdea {
  wishControl = input.required<FormControl>();
  linkControl = input.required<FormControl>();

  wishMaxLength = input<number | null>(null);

  wishPlaceholder = InputPlaceholder.WishPlaceholder;
  linkPlaceholder = InputPlaceholder.LinkPlaceholder;
  wishLabel = BaseLabel.GiftIdeaWish;
  linkLabel = BaseLabel.GiftIdeaLink;
  y = ItemPosition.Center;
}
