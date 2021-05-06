import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap} from 'rxjs/operators';
import {of} from 'rxjs';

import {AdvertService as ShardAdvertService} from 'src/app/shared/services/advert.service';
import {getAdvertAction, getAdvertFailureAction, getAdvertSuccessAction} from '../actions/getAdvert.action';
import {AdvertInterface} from 'src/app/shared/types/advert.interface';

@Injectable()
export class GetAdvertEffect {
  constructor(private actions$: Actions, private shardAdvertService: ShardAdvertService) {}

  getAdvert$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getAdvertAction),
      switchMap(({slug}) => {
        return this.shardAdvertService.getAdvert(slug).pipe(
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
