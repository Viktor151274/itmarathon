import { inject, Injectable, signal } from '@angular/core';

import { ApiService } from '../../core/services/api';
import type { RoomDetails, UserDetails } from '../../app.models';
import { Observable, tap } from 'rxjs';
import { HttpResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class JoinRoomService {
  readonly #apiService = inject(ApiService);

  readonly #roomData = signal<RoomDetails>({
    adminId: 0,
    createdOn: '',
    description: '',
    giftExchangeDate: '',
    giftMaximumBudget: 0,
    id: 0,
    invitationCode: '',
    invitationNote: '',
    isFull: false,
    modifiedOn: '',
    name: '',
  });

  public readonly roomData = this.#roomData.asReadonly();

  public addUserToRoom(roomCode: string, userData: UserDetails): void {
    this.#apiService.addUserToRoom(roomCode, userData);
  }

  public getRoomByRoomCode(
    roomId: string
  ): Observable<HttpResponse<RoomDetails>> {
    return this.#apiService
      .getRoomByRoomCode(roomId)
      .pipe(tap((result) => result.body && this.#roomData.set(result.body)));
  }
}
