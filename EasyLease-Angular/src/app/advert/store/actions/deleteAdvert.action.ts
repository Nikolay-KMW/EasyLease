import {createAction, props} from '@ngrx/store';

import {ActionTypes} from '../actionTypes';

export const deleteAdvertAction = createAction(ActionTypes.DELETE_ADVERT, props<{slug: string}>());

export const deleteAdvertSuccessAction = createAction(ActionTypes.DELETE_ADVERT_SUCCESS);

export const deleteAdvertFailureAction = createAction(ActionTypes.DELETE_ADVERT_FAILURE);
