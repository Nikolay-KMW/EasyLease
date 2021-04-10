import {createAction, props} from '@ngrx/store';

import {TagType} from 'src/app/shared/types/Tag.type';
import {ActionTypes} from '../actionTypes';

export const getTagsAction = createAction(ActionTypes.GET_TAGS, props<{url: string}>());

export const getTagsSuccessAction = createAction(ActionTypes.GET_TAGS_SUCCESS, props<{tags: TagType[]}>());

export const getTagsFailureAction = createAction(ActionTypes.GET_TAGS_FAILURE);
