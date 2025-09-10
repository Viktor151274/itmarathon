import { Component } from '@angular/core';
import { DatePicker } from "../shared/components/date-picker/date-picker";
import { BaseLabel, InputPlaceholder } from '../app.enum';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-join-room',
  templateUrl: './join-room.html',
  styleUrl: './join-room.scss',
  imports: [DatePicker, ReactiveFormsModule],
})
export class JoinRoom {
  placeholder = InputPlaceholder.Date;
  lab = BaseLabel.ExchangeDate
  control = new FormControl(null)
}
