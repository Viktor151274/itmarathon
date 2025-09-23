import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'budget',
  standalone: true,
})
export class BudgetPipe implements PipeTransform {
  transform(value: number | null | undefined, currency = 'UAH'): string {
    if (!value) {
      return 'Unlimited';
    }
    return `${value} ${currency}`;
  }
}
