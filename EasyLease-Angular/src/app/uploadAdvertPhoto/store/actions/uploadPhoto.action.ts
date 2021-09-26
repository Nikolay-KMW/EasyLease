import {createAction, props} from '@ngrx/store';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {ActionTypes} from '../actionTypes';

export const uploadPhotoAction = createAction(ActionTypes.UPLOAD_PHOTO, props<{slug: string; files: File[]}>());

export const uploadPhotoSuccessAction = createAction(
  ActionTypes.UPLOAD_PHOTO_SUCCESS,
  props<{advert: AdvertInterface}>()
);

export const uploadPhotoFailureAction = createAction(
  ActionTypes.UPLOAD_PHOTO_FAILURE,
  props<{errors: BackendErrorInterface}>()
);
