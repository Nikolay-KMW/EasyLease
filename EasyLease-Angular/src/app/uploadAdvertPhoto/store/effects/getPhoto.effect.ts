import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, mergeMap, switchMap, tap} from 'rxjs/operators';
import {from, of} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {
  downloadPhotoForAdvertAction,
  getPhotoForAdvertAction,
  getPhotoForAdvertFailureAction,
  getPhotoForAdvertSuccessAction,
  quantityDownloadPhotoAction,
} from '../actions/getPhoto.action';
import {DownloadPhotoService} from '../../services/downloadPhoto.service';
import {AdvertService as SharedAdvertService} from 'src/app/shared/services/advert.service';

@Injectable()
export class GetPhotoEffect {
  fileBuffer: File[] = [];
  quantityFiles: number = 0;

  constructor(
    private actions$: Actions,
    private downloadPhotoService: DownloadPhotoService,
    private sharedAdvertService: SharedAdvertService
  ) {}

  getPhotoForAdvert$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getPhotoForAdvertAction),
      switchMap(({slug}) => {
        return this.sharedAdvertService.getAdvert(slug).pipe(
          map((advert: AdvertInterface) => {
            this.quantityFiles = advert.images.length;

            return downloadPhotoForAdvertAction({imagesPath: advert.images});
          }),
          catchError(() => {
            return of(getPhotoForAdvertFailureAction());
          })
        );
      })
    )
  );

  downloadPhotoForAdvert$ = createEffect(() =>
    this.actions$.pipe(
      ofType(downloadPhotoForAdvertAction),
      switchMap(({imagesPath}) => {
        return from(imagesPath).pipe(
          mergeMap((imagePath) =>
            this.downloadPhotoService.downloadPhotoForAdvert(imagePath).pipe(
              map((photo: File) => {
                this.fileBuffer.push(photo);

                if (this.quantityFiles == this.fileBuffer.length) {
                  const buffer = this.fileBuffer;
                  this.fileBuffer = [];
                  return getPhotoForAdvertSuccessAction({photos: Object.assign([], buffer)});
                }

                return quantityDownloadPhotoAction({quantity: this.fileBuffer.length});
              }),
              catchError(() => {
                return of(getPhotoForAdvertFailureAction());
              })
            )
          )
        );
      })
    )
  );
}
