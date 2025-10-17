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
import { ModalService } from '../core/services/modal';
import { GifteeInfoModal } from './components/giftee-info-modal/giftee-info-modal';
import { getPersonalInfo } from '../utils/get-personal-info';
import { MyWishlistModal } from './components/my-wishlist/components/my-wishlist-modal/my-wishlist-modal';

@Component({
  selector: 'app-room',
  imports: [
    RoomInfo,
    RandomizeCard,
    GifteeInfo,
    ParticipantList,
    MyWishlist,
    MyWishlistModal,
  ],
  templateUrl: './room.html',
  styleUrl: './room.scss',
})
export class Room implements OnInit {
  readonly #route = inject(ActivatedRoute);
  readonly #roomService = inject(RoomService);
  readonly #userService = inject(UserService);
  readonly #modalService = inject(ModalService);

  public readonly roomData = this.#roomService.roomData;
  public readonly users = this.#userService.users;
  public readonly isAdmin = this.#userService.isAdmin;
  public readonly invitationLink = this.#roomService.invitationLink;
  public readonly isRoomDrawn = this.#roomService.isRoomDrawn;
  public readonly currentUser = this.#userService.currentUser;

  public readonly isRandomizeCardDisabled = computed(
    () => this.users().length < MIN_USERS_NUMBER
  );
  public readonly gifteeName = computed(() => this.#getGifteeName());

  readonly userCode = this.#userService.userCode;

  ngOnInit(): void {
    this.#route.paramMap.subscribe((params) => {
      this.#userService.setUserCode(params.get('userCode') ?? '');
    });

    this.#roomService.getRoomByUserCode(this.#userService.userCode());
    this.#userService.getUsers();
  }

  public onDrawNames(): void {
    this.#userService.drawNames();
  }

  public onReadDetails(): void {
    this.#modalService.openWithResult(
      GifteeInfoModal,
      {
        personalInfo: getPersonalInfo(this.currentUser()),
        wishListInfo: {
          interests: this.currentUser()?.interests || '',
          wishList: this.currentUser()?.wishList || [],
        },
      },
      {
        buttonAction: () => this.#modalService.close(),
        closeModal: () => this.#modalService.close(),
      }
    );
  }

  public onViewWishlist(): void {
    this.#modalService.openWithResult(
      MyWishlistModal,
      {
        wishListInfo: {
          interests: this.currentUser()?.interests || '',
          wishList: this.currentUser()?.wishList || [],
        },
        budget: this.roomData().giftMaximumBudget,
      },
      {
        buttonAction: () => this.#modalService.close(),
        closeModal: () => this.#modalService.close(),
      }
    );
  }

  #getGifteeName(): string {
    const gifteeId = this.#userService.currentUser()?.giftToUserId || 0;
    const gifteeUser = this.users().find((user) => user.id === gifteeId);
    const [firstName, lastName] = [gifteeUser?.firstName, gifteeUser?.lastName];

    return firstName && lastName ? `${firstName} ${lastName}` : '';
  }
}
