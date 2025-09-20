import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { Header } from './components/header/header';
import { Footer } from './components/footer/footer';
import { ToastService } from './core/services/toast';
import { MessageSize } from './app.enum';
import { Message } from './shared/components/message/message';
import { Loader } from './shared/components/loader/loader';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header, Footer, Message, Loader],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  readonly SIZE_TOASTER = MessageSize.Toaster;

  public readonly toast = inject(ToastService);
}
