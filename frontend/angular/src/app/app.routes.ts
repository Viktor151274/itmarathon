import { Routes } from '@angular/router';
import { Path } from './app.enum';

export const routes: Routes = [
  { path: '', redirectTo: Path.Home, pathMatch: 'full' },
  {
    path: Path.Home,
    loadComponent: () => import('./home/home').then((module) => module.Home),
  },
  {
    path: Path.Welcome,
    loadComponent: () =>
      import('./welcome/welcome').then((module) => module.Welcome),
  },

  {
    path: Path.CreateRoom,
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./create-room/create-room').then(
            (module) => module.CreateRoom
          ),
      },
      {
        path: Path.Success,
        loadComponent: () =>
          import('./create-room/success').then((module) => module.Success),
      },
    ],
  },
  {
    path: Path.JoinRoom,
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./join-room/join-room').then((module) => module.JoinRoom),
      },
      {
        path: Path.Success,
        loadComponent: () =>
          import('./join-room/success').then((module) => module.Success),
      },
    ],
  },
  {
    path: Path.Dashboard,
    loadComponent: () =>
      import('./dashboard/dashboard').then((module) => module.Dashboard),
  },
  {
    path: Path.NotFound,
    loadComponent: () =>
      import('./not-found/not-found').then((module) => module.NotFound),
  },
];
