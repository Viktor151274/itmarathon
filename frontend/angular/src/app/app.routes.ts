import { Routes } from '@angular/router';

import { PageTitle, Path } from './app.enum';
import { CreateRoomService } from './create-room/services/create-room';
import { welcomeGuard } from './core/guards/welcome-guard';
import { createRoomSuccessCanActivateGuard } from './core/guards/create-room-success-can-activate-guard';
import { joinRoomSuccessCanActivateGuard } from './core/guards/join-room-success-can-activate-guard';

export const routes: Routes = [
  { path: '', redirectTo: Path.Home, pathMatch: 'full' },
  {
    path: Path.Home,
    loadComponent: () =>
      import('./home/home').then((component) => component.Home),
    title: PageTitle.Home,
  },
  {
    path: Path.CreateRoom,
    providers: [CreateRoomService],
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./create-room/create-room').then(
            (component) => component.CreateRoom
          ),
        title: PageTitle.CreateRoom,
      },
      {
        path: Path.Success,
        canActivate: [createRoomSuccessCanActivateGuard],
        loadComponent: () =>
          import('./create-room/success').then(
            (component) => component.Success
          ),
        title: PageTitle.CreateSuccess,
      },
    ],
  },
  {
    path: `${Path.Join}/:roomId`,
    canActivate: [welcomeGuard],
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./welcome/welcome').then((component) => component.Welcome),
        title: PageTitle.Welcome,
      },
      {
        path: Path.Details,
        loadComponent: () =>
          import('./join-room/join-room').then(
            (component) => component.JoinRoom
          ),
        title: PageTitle.JoinRoom,
      },
      {
        path: Path.Success,
        canActivate: [joinRoomSuccessCanActivateGuard],
        loadComponent: () =>
          import('./join-room/success').then((component) => component.Success),
        title: PageTitle.JoinSuccess,
      },
    ],
  },
  {
    path: `${Path.Room}/:userCode`,
    loadComponent: () =>
      import('./room/room').then((component) => component.Room),
    title: PageTitle.Room,
  },
  {
    path: Path.NotFound,
    loadComponent: () =>
      import('./not-found/not-found').then((component) => component.NotFound),
  },
];
