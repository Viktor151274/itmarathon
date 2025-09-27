import { inject, Injectable, signal } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { HttpResponse } from '@angular/common/http';

import { ApiService } from '../../core/services/api';
import { JOIN_ROOM_DATA_DEFAULT } from '../../app.constants';
import type {
  JoinRoomWelcomePageData,
  RoomDetails,
  UserDetails,
} from '../../app.models';

@Injectable({
  providedIn: 'root',
})
export class JoinRoomService {
  readonly #apiService = inject(ApiService);

  readonly #roomData = signal<JoinRoomWelcomePageData>(JOIN_ROOM_DATA_DEFAULT);

  public readonly roomData = this.#roomData.asReadonly();

  public addUserToRoom(roomCode: string, userData: UserDetails): void {
    this.#apiService.addUserToRoom(roomCode, userData);
  }

  public getRoomByRoomCode(
    roomId: string
  ): Observable<HttpResponse<RoomDetails>> {
    return this.#apiService.getRoomByRoomCode(roomId).pipe(
      tap(({ body }) => {
        if (body) {
          this.#roomData.set(body);
        }
      })
    );
  }
}
