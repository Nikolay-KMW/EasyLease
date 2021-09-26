import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap, tap} from 'rxjs/operators';
import {of} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';
import {Router} from '@angular/router';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {UploadPhotoService} from '../../services/uploadPhoto.service';
import {uploadPhotoAction, uploadPhotoFailureAction, uploadPhotoSuccessAction} from '../actions/uploadPhoto.action';

@Injectable()
export class UploadPhotoEffect {
  constructor(private actions$: Actions, private uploadPhotoService: UploadPhotoService, private router: Router) {}

  UploadPhotoForAdvert$ = createEffect(() =>
    this.actions$.pipe(
      ofType(uploadPhotoAction),
      switchMap(({slug, files}) => {
        return this.uploadPhotoService.uploadPhotoForAdvert(slug, files).pipe(
          map((advert: AdvertInterface) => {
            return uploadPhotoSuccessAction({advert});
          }),
          catchError((errorResponse: HttpErrorResponse) => {
            return of(uploadPhotoFailureAction({errors: errorResponse.error}));
          })
        );
      })
    )
  );

  redirectAfterUploadPhoto$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(uploadPhotoSuccessAction),
        tap(({advert}) => this.router.navigate(['/advert', advert.slug]))
      ),
    {dispatch: false}
  );
}
