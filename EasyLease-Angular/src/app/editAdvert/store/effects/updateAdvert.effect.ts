import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap, tap} from 'rxjs/operators';
import {of} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';
import {Router} from '@angular/router';

import {EditAdvertService} from '../../services/editAdvert.service';
import {updateAdvertAction, updateAdvertSuccessAction, updateAdvertFailureAction} from '../actions/updateAdvert.action';
import {AdvertInterface} from 'src/app/shared/types/advert.interface';

@Injectable()
export class UpdateAdvertEffect {
  constructor(private actions$: Actions, private editAdvertService: EditAdvertService, private router: Router) {}

  updateAdvert$ = createEffect(() =>
    this.actions$.pipe(
      ofType(updateAdvertAction),
      switchMap(({slug, advertInput}) => {
        return this.editAdvertService.updateAdvert(slug, advertInput).pipe(
          map((advert: AdvertInterface) => {
            return updateAdvertSuccessAction({advert});
          }),
          catchError((errorResponse: HttpErrorResponse) => {
            return of(updateAdvertFailureAction({errors: errorResponse.error.errors}));
          })
        );
      })
    )
  );

  redirectAfterUpdateAdvert$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(updateAdvertSuccessAction),
        tap(({advert}) => this.router.navigate(['/articles', advert.slug]))
      ),
    {dispatch: false}
  );
}
