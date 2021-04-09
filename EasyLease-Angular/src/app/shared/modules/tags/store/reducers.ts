import {routerNavigatedAction} from '@ngrx/router-store';
import {Action, createReducer, on} from '@ngrx/store';

import {TagsStateInterface} from '../types/tagsState.interface';
import {getTagsAction, getTagsFailureAction, getTagsSuccessAction} from './actions/getTags.action';

const initialState: TagsStateInterface = {
  isLoading: false,
  error: null,
  date: null,
};

export const tagsReducer = createReducer(
  initialState,
  on(
    getTagsAction,
    (state): TagsStateInterface => ({
      ...state,
      isLoading: true,
    })
  ),
  on(
    getTagsSuccessAction,
    (state, action): TagsStateInterface => ({
      ...state,
      isLoading: false,
      date: action.tags,
    })
  ),
  on(
    getTagsFailureAction,
    (state): TagsStateInterface => ({
      ...state,
      isLoading: false,
    })
  )
);

export function reducers(state: TagsStateInterface, action: Action) {
  return tagsReducer(state, action);
}
