import {createAction, props} from '@ngrx/store';

import {GetTagsResponseInterface} from '../../types/getTagsResponse.interface';
import {ActionTypes} from '../actionTypes';

export const getTagsAction = createAction(ActionTypes.GET_TAGS, props<{url: string}>());

export const getTagsSuccessAction = createAction(
  ActionTypes.GET_TAGS_SUCCESS,
  props<{tags: GetTagsResponseInterface}>()
);

export const getTagsFailureAction = createAction(ActionTypes.GET_TAGS_FAILURE);
