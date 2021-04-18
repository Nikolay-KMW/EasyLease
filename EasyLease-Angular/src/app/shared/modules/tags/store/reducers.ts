import {Action, createReducer, on} from '@ngrx/store';

import {TagsStateInterface} from '../types/tagsState.interface';
import {getTagsAction, getTagsFailureAction, getTagsSuccessAction} from './actions/getTags.action';
import {setSelectedTagAction} from './actions/setSelectedTag.action';

const initialState: TagsStateInterface = {
  isLoading: false,
  error: null,
  date: null,
  selectedTag: null,
};

export const tagsReducer = createReducer(
  initialState,
  on(
    getTagsAction,
    (state): TagsStateInterface => ({
      ...state,
      isLoading: true,
      date: null,
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
  ),
  on(
    setSelectedTagAction,
    (state, action): TagsStateInterface => ({
      ...state,
      selectedTag: action.selectedTag,
    })
  )
);

export function reducers(state: TagsStateInterface, action: Action) {
  return tagsReducer(state, action);
}
