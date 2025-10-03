import { computed, inject, Injectable, signal } from '@angular/core';
import { User } from '../../app.models';
import { ApiService } from '../../core/services/api';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  readonly #apiService = inject(ApiService);
  readonly #userCode = signal<string>('');

  public readonly userCode = this.#userCode.asReadonly();
  public readonly users = signal<User[]>([]);
  public readonly isAdmin = computed(
    () =>
      this.users().find((user) => user.userCode === this.#userCode())
        ?.isAdmin ?? false
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
            this.users.set(result.body);
          }
        })
      )
      .subscribe();
  }
}
