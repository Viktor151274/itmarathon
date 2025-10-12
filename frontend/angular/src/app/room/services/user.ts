import { computed, inject, Injectable, signal } from '@angular/core';
import { tap } from 'rxjs';

import { ApiService } from '../../core/services/api';
import { RoomService } from './room';
import { ToastService } from '../../core/services/toast';
import { MessageType, ToastMessage } from '../../app.enum';
import type { User } from '../../app.models';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  readonly #apiService = inject(ApiService);
  readonly #roomService = inject(RoomService);
  readonly #toasterService = inject(ToastService);

  readonly #userCode = signal<string>('');
  readonly #users = signal<User[]>([]);

  public readonly userCode = this.#userCode.asReadonly();
  public readonly users = this.#users.asReadonly();

  public readonly currentUser = computed(() =>
    this.users().find((user) => user.userCode === this.#userCode())
  );
  public readonly isAdmin = computed(
    () => this.currentUser()?.isAdmin ?? false
  );

  public setUserCode(code: string): void {
    this.#userCode.set(code);
  }

  public getUsers(): void {
    this.#apiService
      .getUsers(this.#userCode())
      .pipe(
        tap((result) => {
          if (result?.body) {
            this.#users.set(result.body);
          }
        })
      )
      .subscribe();
  }

  public drawNames(): void {
    this.#apiService
      .drawNames(this.#userCode())
      .pipe(
        tap(({ status }) => {
          if (status === 200) {
            this.#roomService.getRoomByUserCode(this.#userCode());
            this.getUsers();
            this.#toasterService.show(
              ToastMessage.SuccessDrawNames,
              MessageType.Success
            );
          }
        })
      )
      .subscribe();
  }
}
