import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { ErrorMessage, MessageType, Path } from '../../app.enum';
import { JoinRoomService } from '../../join-room/services/join-room';
import { catchError, map, of } from 'rxjs';
import { RedirectWithToastService } from '../services/redirect-with-toast';
import { GuardReturnType } from '../../app.models';

export const welcomeGuard: CanActivateFn = (route): GuardReturnType => {
  const roomApi = inject(JoinRoomService);
  const redirectWithToast = inject(RedirectWithToastService);

  return roomApi.getRoomByRoomCode(route.params['roomId']).pipe(
    map((result) => {
      if (!result.body?.closedOn && !result.body?.isFull) {
        return true;
      }

      const errorMessage: ErrorMessage = result.body?.closedOn
        ? ErrorMessage.UnavailableRoom
        : ErrorMessage.FullRoom;

      return redirectWithToast.redirect(
        Path.Home,
        errorMessage,
        MessageType.Error
      );
    }),
    catchError(() =>
      of(
        redirectWithToast.redirect(
          Path.Home,
          ErrorMessage.UnavailableRoom,
          MessageType.Error
        )
      )
    )
  );
};
