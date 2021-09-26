import {createAction, props} from '@ngrx/store';

import {ActionTypes} from '../actionTypes';

export const getPhotoForAdvertAction = createAction(ActionTypes.GET_PHOTO, props<{slug: string}>());

export const getPhotoForAdvertSuccessAction = createAction(ActionTypes.GET_PHOTO_SUCCESS, props<{files: File[]}>());

export const getPhotoForAdvertFailureAction = createAction(ActionTypes.GET_PHOTO_FAILURE);
