import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ENDPOINT } from '../../app.enum';
import { BASE_URL } from './tokens/base-url.token';
import {
  UserDetails,
  RoomCreationRequest,
  RoomSummary,
  RoomDetails,
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
      `${this.#baseUrl}${ENDPOINT.rooms}`,
      roomCreationData,
      { observe: 'response' }
    );
  }

  public getRoomByCode(
    roomCode: string
  ): Observable<HttpResponse<RoomDetails>> {
    const params = new HttpParams().set('roomCode', roomCode);

    return this.#http.get<RoomDetails>(`${this.#baseUrl}${ENDPOINT.rooms}`, {
      params,
      observe: 'response',
    });
  }

  public addUserToRoom(
    roomCode: string,
    userData: UserDetails
  ): Observable<HttpResponse<UserDetails>> {
    const params = new HttpParams().set('roomCode', roomCode);

    return this.#http.post<UserDetails>(
      `${this.#baseUrl}${ENDPOINT.users}`,
      userData,
      { params, observe: 'response' }
    );
  }

  public getRoomByUserCode(
    userCode: string
  ): Observable<HttpResponse<RoomDetails>> {
    const params = new HttpParams().set('userCode', userCode);

    return this.#http.get<RoomDetails>(`${this.#baseUrl}${ENDPOINT.rooms}`, {
      params,
      observe: 'response',
    });
  }
}
