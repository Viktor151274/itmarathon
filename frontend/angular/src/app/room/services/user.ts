import { computed, inject, Injectable, signal } from '@angular/core';
import { tap } from 'rxjs';

import { ApiService } from '../../core/services/api';
import type { User } from '../../app.models';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  readonly #apiService = inject(ApiService);

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

  public setUserCode(code: string) {
    this.#userCode.set(code);
  }

  public getUsers() {
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
}
