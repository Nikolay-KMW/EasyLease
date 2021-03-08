import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap} from 'rxjs/operators';
import {of} from 'rxjs';

import {CurrentUserInterface} from 'src/app/shared/types/currentUser.interface';
import {AuthService} from '../../services/auth.service';
import {PersistanceService} from 'src/app/shared/services/persistance.service';
import {
  getCurrentFailureUserAction,
  getCurrentSuccessUserAction,
  getCurrentUserAction,
} from '../actions/getCurrentUser.action';

@Injectable()
export class GetCurrentUserEffect {
  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private persistanceService: PersistanceService
  ) {}

  getCurrentUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getCurrentUserAction),
      switchMap(() => {
        const token = this.persistanceService.get('accessToken');
        if (!token) {
          return of(getCurrentFailureUserAction());
        }
        return this.authService.getCurrentUser().pipe(
          map((currentUser: CurrentUserInterface) => {
            return getCurrentSuccessUserAction({currentUser});
          }),
          catchError(() => {
            return of(getCurrentFailureUserAction());
          })
        );
      })
    )
  );
}
