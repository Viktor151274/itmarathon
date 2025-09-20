import { inject, Injectable } from '@angular/core';

import { ApiService } from '../../core/services/api';
import type { UserDetails } from '../../app.models';

@Injectable({
  providedIn: 'root',
})
export class JoinRoomService {
  readonly #apiService = inject(ApiService);

  public addUserToRoom(roomCode: string, userData: UserDetails): void {
    this.#apiService.addUserToRoom(roomCode, userData);
  }
}
