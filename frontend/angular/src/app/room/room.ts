import { Component, computed, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { RoomInfo } from './components/room-info/room-info';
import { RoomService } from './services/room';
import { UserService } from './services/user';
import { ParticipantList } from '../shared/components/participant-list/participant-list';
import { RandomizeCard } from './components/randomize-card/randomize-card';
import { GifteeInfo } from './components/giftee-info/giftee-info';
import { MIN_USERS_NUMBER } from '../app.constants';
import { MyWishlist } from './components/my-wishlist/my-wishlist';

@Component({
  selector: 'app-room',
  imports: [RoomInfo, RandomizeCard, GifteeInfo, ParticipantList, MyWishlist],
  templateUrl: './room.html',
  styleUrl: './room.scss',
})
export class Room implements OnInit {
  readonly route = inject(ActivatedRoute);
  readonly roomService = inject(RoomService);
  readonly userService = inject(UserService);

  public readonly roomData = this.roomService.roomData;
  public readonly users = this.userService.users;
  public readonly isAdmin = this.userService.isAdmin;
  public readonly invitationLink = this.roomService.invitationLink;
  public readonly isRoomDrawn = this.roomService.isRoomDrawn;

  public readonly isRandomizeCardDisabled = computed(
    () => this.users().length < MIN_USERS_NUMBER
  );
  public readonly gifteeName = computed(() => this.#getGifteeName());

  readonly userCode = this.userService.userCode;

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.userService.setUserCode(params.get('userCode') ?? '');
    });

    this.roomService.getRoomByUserCode(this.userService.userCode());
    this.userService.getUsers();
  }

  public onDrawNames(): void {
    // TODO: implement the randomization logic https://jiraeu.epam.com/browse/EPMRDUAITM-189
  }

  public onReadDetails(): void {
    // TODO: implement the opening of giftee info modal https://jiraeu.epam.com/browse/EPMRDUAITM-159
  }

  #getGifteeName(): string {
    const gifteeId = this.userService.currentUser()?.giftToUserId || 0;
    const gifteeUser = this.users().find((user) => user.id === gifteeId);
    const [firstName, lastName] = [gifteeUser?.firstName, gifteeUser?.lastName];

    return firstName && lastName ? `${firstName} ${lastName}` : '';
  }
}
