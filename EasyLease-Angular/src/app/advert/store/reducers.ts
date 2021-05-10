import {routerNavigatedAction} from '@ngrx/router-store';
import {Action, createReducer, on} from '@ngrx/store';

import {AdvertStateInterface} from '../types/advertState.interface';
import {getAdvertAction, getAdvertFailureAction, getAdvertSuccessAction} from './actions/getAdvert.action';

const initialState: AdvertStateInterface = {
  isLoading: false,
  error: null,
  date: null,
};

export const advertReducer = createReducer(
  initialState,
  on(
    getAdvertAction,
    (state): AdvertStateInterface => ({
      ...state,
      isLoading: true,
    })
  ),
  on(
    getAdvertSuccessAction,
    (state, action): AdvertStateInterface => ({
      ...state,
      isLoading: false,
      date: action.advert,
    })
  ),
  on(
    getAdvertFailureAction,
    (state): AdvertStateInterface => ({
      ...state,
      isLoading: false,
    })
  ),
  on(
    routerNavigatedAction,
    (state): AdvertStateInterface => ({
      ...state,
      error: null,
      date: null,
    })
  )
);

export function reducers(state: AdvertStateInterface, action: Action) {
  return advertReducer(state, action);
}
