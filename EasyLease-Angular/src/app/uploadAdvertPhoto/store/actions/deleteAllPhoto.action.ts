import {createAction, props} from '@ngrx/store';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {ActionTypes} from '../actionTypes';

export const deleteAllPhotoAction = createAction(ActionTypes.DELETE_All_PHOTO, props<{slug: string}>());

export const deleteAllPhotoSuccessAction = createAction(
  ActionTypes.DELETE_All_PHOTO_SUCCESS,
  props<{advert: AdvertInterface}>()
);

export const deleteAllPhotoFailureAction = createAction(ActionTypes.DELETE_All_PHOTO_FAILURE);
