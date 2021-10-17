import {createAction, props} from '@ngrx/store';
import {ImageInterface} from 'src/app/shared/types/image.interface';

import {ActionTypes} from '../actionTypes';

export const getPhotoForAdvertAction = createAction(ActionTypes.GET_PHOTO, props<{slug: string}>());

export const quantityDownloadPhotoAction = createAction(
  ActionTypes.QUANTITY_DOWNLOAD_PHOTO,
  props<{quantity: number}>()
);

export const downloadPhotoForAdvertAction = createAction(
  ActionTypes.DOWNLOAD_PHOTO,
  props<{images: ImageInterface[]}>()
);

export const getPhotoForAdvertSuccessAction = createAction(ActionTypes.GET_PHOTO_SUCCESS, props<{photos: File[]}>());

export const getPhotoForAdvertFailureAction = createAction(ActionTypes.GET_PHOTO_FAILURE);
