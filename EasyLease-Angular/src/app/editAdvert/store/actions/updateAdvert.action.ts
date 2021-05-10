import {createAction, props} from '@ngrx/store';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {AdvertInputInterface} from 'src/app/shared/types/advertInput.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {ActionTypes} from '../actionTypes';

export const updateAdvertAction = createAction(
  ActionTypes.UPDATE_ADVERT,
  props<{slug: string; advertInput: AdvertInputInterface}>()
);

export const updateAdvertSuccessAction = createAction(
  ActionTypes.UPDATE_ADVERT_SUCCESS,
  props<{advert: AdvertInterface}>()
);

export const updateAdvertFailureAction = createAction(
  ActionTypes.UPDATE_ADVERT_FAILURE,
  props<{errors: BackendErrorInterface}>()
);
