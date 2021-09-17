import {createAction, props} from '@ngrx/store';

import {AdvertAdditionalData} from '../../types/advertAdditionalData.interface';
import {ActionTypes} from '../actionTypes';

export const getAdditionalDataAction = createAction(ActionTypes.GET_ADDITIONAL_DATA_FOR_ADVERT);

export const getAdditionalDataSuccessAction = createAction(
  ActionTypes.GET_ADDITIONAL_DATA_FOR_ADVERT_SUCCESS,
  props<{additionalData: AdvertAdditionalData}>()
);

export const getAdditionalDataFailureAction = createAction(ActionTypes.GET_ADDITIONAL_DATA_FOR_ADVERT_FAILURE);
