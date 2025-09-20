import { inject, Injectable } from '@angular/core';

import { ApiService } from '../../core/services/api';
import type { RoomCreationRequest } from '../../app.models';

@Injectable({
  providedIn: 'root',
})
export class CreateRoomService {
  readonly #apiService = inject(ApiService);

  public createRoom(roomCreationData: RoomCreationRequest): void {
    this.#apiService.createRoom(roomCreationData);
  }
}
