import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap} from 'rxjs/operators';
import {of} from 'rxjs';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {AdvertService as SharedAdvertService} from 'src/app/shared/services/advert.service';
import {getAdvertAction, getAdvertSuccessAction, getAdvertFailureAction} from '../actions/getAdvert.action';

@Injectable()
export class GetAdvertEffect {
  constructor(private actions$: Actions, private sharedAdvertService: SharedAdvertService) {}

  getAdvert$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getAdvertAction),
      switchMap(({slug}) => {
        return this.sharedAdvertService.getAdvert(slug).pipe(
          map((advert: AdvertInterface) => {
            return getAdvertSuccessAction({advert});
          }),
          catchError(() => {
            return of(getAdvertFailureAction());
          })
        );
      })
    )
  );
}
