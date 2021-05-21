import {createAction, props} from '@ngrx/store';

import {ActionTypes} from '../actionTypes';

export const setTitleAction = createAction(ActionTypes.SET_TITLE, props<{title: string}>());

export const setDescriptionAction = createAction(ActionTypes.SET_DESCRIPTION, props<{description: string}>());
