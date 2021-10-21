import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap, tap} from 'rxjs/operators';
import {of} from 'rxjs';
import {Router} from '@angular/router';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {DeleteAllPhotoService} from '../../services/deleteAllPhoto.service';
import {
  deleteAllPhotoAction,
  deleteAllPhotoFailureAction,
  deleteAllPhotoSuccessAction,
} from '../actions/deleteAllPhoto.action';

@Injectable()
export class DeleteAllPhotoEffect {
  constructor(
    private actions$: Actions,
    private deleteAllPhotoService: DeleteAllPhotoService,
    private router: Router
  ) {}

  deleteAllPhotoForAdvert$ = createEffect(() =>
    this.actions$.pipe(
      ofType(deleteAllPhotoAction),
      switchMap(({slug}) => {
        return this.deleteAllPhotoService.deleteAllPhotoForAdvert(slug).pipe(
          map((advert: AdvertInterface) => {
            return deleteAllPhotoSuccessAction({advert});
          }),
          catchError(() => {
            return of(deleteAllPhotoFailureAction());
          })
        );
      })
    )
  );

  redirectAfterDeleteAllPhoto$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(deleteAllPhotoSuccessAction),
        tap(({advert}) => this.router.navigate(['/advert', advert.slug]))
      ),
    {dispatch: false}
  );
}
