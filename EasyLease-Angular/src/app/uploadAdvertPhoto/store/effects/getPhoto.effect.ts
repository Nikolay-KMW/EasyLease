import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, mergeMap, switchMap} from 'rxjs/operators';
import {from, of} from 'rxjs';

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

            if (this.quantityFiles === 0) {
              return getPhotoForAdvertSuccessAction({photos: []});
            }

            return downloadPhotoForAdvertAction({images: advert.images});
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
      switchMap(({images}) => {
        return from(images).pipe(
          mergeMap((image) =>
            this.downloadPhotoService.downloadPhotoForAdvert(image).pipe(
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
