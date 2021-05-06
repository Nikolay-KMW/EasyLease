import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap, tap} from 'rxjs/operators';
import {of} from 'rxjs';

import {AdvertService} from '../../services/advert.service';
import {deleteAdvertAction, deleteAdvertFailureAction, deleteAdvertSuccessAction} from '../actions/deleteAdvert.action';
import {Router} from '@angular/router';

@Injectable()
export class DeleteAdvertEffect {
  constructor(private actions$: Actions, private advertService: AdvertService, private router: Router) {}

  deleteAdvert$ = createEffect(() =>
    this.actions$.pipe(
      ofType(deleteAdvertAction),
      switchMap(({slug}) => {
        return this.advertService.deletedAdvert(slug).pipe(
          map(() => {
            return deleteAdvertSuccessAction();
          }),
          catchError(() => {
            return of(deleteAdvertFailureAction());
          })
        );
      })
    )
  );

  redirectAfterDelete$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(deleteAdvertSuccessAction),
        tap(() => this.router.navigate(['/']))
      ),
    {dispatch: false}
  );
}
