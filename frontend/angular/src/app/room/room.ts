import { Component, inject, OnInit } from '@angular/core';
import { RoomInfo } from './components/room-info/room-info';
import { ActivatedRoute } from '@angular/router';
import { RoomService } from './services/room';
import { UserService } from './services/user';

@Component({
  selector: 'app-room',
  imports: [RoomInfo],
  templateUrl: './room.html',
  styleUrl: './room.scss',
})
export class Room implements OnInit {
  readonly route = inject(ActivatedRoute);
  readonly roomService = inject(RoomService);
  readonly userService = inject(UserService);

  public readonly roomData = this.roomService.roomData;
  public readonly isAdmin = this.userService.isAdmin;
  public readonly invitationLink = this.roomService.invitationLink;

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.userService.setUserCode(params.get('userCode') ?? '');
    });

    this.roomService.getRoomByUserCode(this.userService.userCode());
    this.userService.getUsers();
  }
}
