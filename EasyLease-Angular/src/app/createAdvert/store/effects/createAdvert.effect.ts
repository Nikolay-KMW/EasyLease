import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap, tap} from 'rxjs/operators';
import {of} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';
import {Router} from '@angular/router';

import {CreateAdvertService} from '../../services/createAdvert.service';
import {createAdvertAction, createAdvertSuccessAction, createAdvertFailureAction} from '../actions/createAdvert.action';
import {AdvertInterface} from 'src/app/shared/types/advert.interface';

@Injectable()
export class CreateAdvertEffect {
  constructor(private actions$: Actions, private createAdvertService: CreateAdvertService, private router: Router) {}

  createAdvert$ = createEffect(() =>
    this.actions$.pipe(
      ofType(createAdvertAction),
      switchMap(({advertInput}) => {
        return this.createAdvertService.createAdvert(advertInput).pipe(
          map((advert: AdvertInterface) => {
            return createAdvertSuccessAction({advert});
          }),
          catchError((errorResponse: HttpErrorResponse) => {
            return of(createAdvertFailureAction({errors: errorResponse.error}));
          })
        );
      })
    )
  );

  redirectAfterCreateAdvert$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(createAdvertSuccessAction),
        tap(({advert}) => this.router.navigate(['/adverts', advert.slug, 'photos']))
      ),
    {dispatch: false}
  );
}
