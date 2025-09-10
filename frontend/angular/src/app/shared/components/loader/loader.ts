import { Component, inject } from '@angular/core';
import { LoaderService } from '../../../core/services/loader';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.html',
  styleUrl: './loader.scss',
})
export class Loader {
  protected visible = inject(LoaderService).visible;
}
