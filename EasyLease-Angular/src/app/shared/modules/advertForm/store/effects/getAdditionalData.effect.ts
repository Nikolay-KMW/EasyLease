import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap} from 'rxjs/operators';
import {of} from 'rxjs';

import {
  getAdditionalDataAction,
  getAdditionalDataFailureAction,
  getAdditionalDataSuccessAction,
} from '../actions/getAdditionalData.action';
import {PersistenceService} from 'src/app/shared/services/persistence.service';
import {AdvertAdditionalData} from '../../types/advertAdditionalData.interface';
import {AdditionalDataService} from '../../services/AdditionalData.service';

@Injectable()
export class GetAdditionalDataEffect {
  constructor(
    private actions$: Actions,
    private AdditionalDataService: AdditionalDataService,
    private persistenceService: PersistenceService
  ) {}

  getAdditionalDataForAdvert$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getAdditionalDataAction),
      switchMap(() => {
        const additionalData = this.persistenceService.get('additionalData') as AdvertAdditionalData;

        if (additionalData) {
          return of(getAdditionalDataSuccessAction({additionalData}));
        }
        return this.AdditionalDataService.getAdditionalDataForAdvert().pipe(
          map((additionalData: AdvertAdditionalData) => {
            this.persistenceService.set('additionalData', additionalData);
            return getAdditionalDataSuccessAction({additionalData});
          }),
          catchError(() => {
            return of(getAdditionalDataFailureAction());
          })
        );
      })
    )
  );
}
