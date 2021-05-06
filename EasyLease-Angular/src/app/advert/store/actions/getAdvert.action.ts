import {createAction, props} from '@ngrx/store';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {ActionTypes} from '../actionTypes';

export const getAdvertAction = createAction(ActionTypes.GET_ADVERT, props<{slug: string}>());

export const getAdvertSuccessAction = createAction(ActionTypes.GET_ADVERT_SUCCESS, props<{advert: AdvertInterface}>());

export const getAdvertFailureAction = createAction(ActionTypes.GET_ADVERT_FAILURE);
