import {routerNavigatedAction} from '@ngrx/router-store';
import {Action, createReducer, on} from '@ngrx/store';

import {FeedStateInterface} from '../types/feedState.interface';
import {getFeedAction, getFeedFailureAction, getFeedSuccessAction} from './actions/getFeed.action';

const initialState: FeedStateInterface = {
  isLoading: false,
  error: null,
  date: null,
};

export const feedReducer = createReducer(
  initialState,
  on(
    getFeedAction,
    (state): FeedStateInterface => ({
      ...state,
      isLoading: true,
    })
  ),
  on(
    getFeedSuccessAction,
    (state, action): FeedStateInterface => ({
      ...state,
      isLoading: false,
      date: action.feed,
    })
  ),
  on(
    getFeedFailureAction,
    (state): FeedStateInterface => ({
      ...state,
      isLoading: false,
    })
  ),
  on(
    routerNavigatedAction,
    (state): FeedStateInterface => ({
      ...state,
      date: null,
      error: null,
    })
  )
);

export function reducers(state: FeedStateInterface, action: Action) {
  return feedReducer(state, action);
}
