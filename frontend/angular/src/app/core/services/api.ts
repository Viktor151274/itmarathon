import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Endpoint } from '../../app.enum';
import { BASE_URL } from './tokens/base-url.token';
import {
  UserDetails,
  RoomCreationRequest,
  RoomSummary,
  RoomDetails,
  UserProfile,
} from '../../app.models';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  readonly #baseUrl = inject(BASE_URL);
  readonly #http = inject(HttpClient);

  public createRoom(
    roomCreationData: RoomCreationRequest
  ): Observable<HttpResponse<RoomSummary>> {
    return this.#http.post<RoomSummary>(
      `${this.#baseUrl}${Endpoint.rooms}`,
      roomCreationData,
      { observe: 'response' }
    );
  }

  public getRoomByRoomCode(
    roomCode: string
  ): Observable<HttpResponse<RoomDetails>> {
    const params = new HttpParams().set('roomCode', roomCode);

    return this.#http.get<RoomDetails>(`${this.#baseUrl}${Endpoint.rooms}`, {
      params,
      observe: 'response',
    });
  }

  public addUserToRoom(
    roomCode: string,
    userData: UserDetails
  ): Observable<HttpResponse<UserProfile>> {
    const params = new HttpParams().set('roomCode', roomCode);

    return this.#http.post<UserProfile>(
      `${this.#baseUrl}${Endpoint.users}`,
      userData,
      { params, observe: 'response' }
    );
  }

  public getRoomByUserCode(
    userCode: string
  ): Observable<HttpResponse<RoomDetails>> {
    const params = new HttpParams().set('userCode', userCode);

    return this.#http.get<RoomDetails>(`${this.#baseUrl}${Endpoint.rooms}`, {
      params,
      observe: 'response',
    });
  }
}
