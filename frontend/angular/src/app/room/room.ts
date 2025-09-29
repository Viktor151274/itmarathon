import { Component } from '@angular/core';
import { RoomInfo } from './components/room-info/room-info';

@Component({
  selector: 'app-room',
  imports: [RoomInfo],
  templateUrl: './room.html',
  styleUrl: './room.scss',
})
export class Room {
  public readonly dummyDate = '10/31/2025 17:41:11';
  public readonly dummyNum: number = 100500;
  public readonly isAdmin: boolean = true;
  public readonly title: string = 'Secret Squad';
  public readonly invitationNote: string = 'Wosup';
  public readonly invitationLink: string = 'Go there';
  public readonly description: string = `Hey everyone!
Welcome to our Secret Nick gift exchange! Check the wishlist, see who’s playing, and get ready for some holiday magic. Let’s make this a festive and fun surprise!`;
}
