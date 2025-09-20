import { Routes } from '@angular/router';
import { PageTitle, Path } from './app.enum';

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
        loadComponent: () =>
          import('./join-room/success').then((component) => component.Success),
        title: PageTitle.JoinSuccess,
      },
    ],
  },
  {
    path: Path.Dashboard,
    loadComponent: () =>
      import('./dashboard/dashboard').then((component) => component.Dashboard),
  },
  {
    path: Path.NotFound,
    loadComponent: () =>
      import('./not-found/not-found').then((component) => component.NotFound),
  },
];
