import {createAction, props} from '@ngrx/store';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {AdvertInputInterface} from 'src/app/shared/types/advertInput.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {ActionTypes} from '../actionTypes';

export const createAdvertAction = createAction(ActionTypes.CREATE_ADVERT, props<{advertInput: AdvertInputInterface}>());

export const createAdvertSuccessAction = createAction(
  ActionTypes.CREATE_ADVERT_SUCCESS,
  props<{advert: AdvertInterface}>()
);

export const createAdvertFailureAction = createAction(
  ActionTypes.CREATE_ADVERT_FAILURE,
  props<{errors: BackendErrorInterface}>()
);
