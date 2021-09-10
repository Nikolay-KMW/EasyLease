import {Injectable} from '@angular/core';

@Injectable()
export class UtilsService {
  range(start: number, end: number): number[] {
    return [...Array(end).keys()].map((el) => el + start);
  }

  scrollTo(
    className: string,
    behavior: 'auto' | 'smooth',
    block?: 'start' | 'center' | 'end' | 'nearest',
    inline?: 'start' | 'center' | 'end' | 'nearest'
  ): void {
    const elementList = document.querySelectorAll('.' + className);
    const element = elementList[0] as HTMLElement;
    element.scrollIntoView({behavior: behavior, block: block, inline: inline});
  }
}
