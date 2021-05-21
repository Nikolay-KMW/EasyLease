import {routerNavigatedAction} from '@ngrx/router-store';
import {Action, createReducer, on} from '@ngrx/store';

import {BannerStateInterface} from '../types/bannerState.interface';
import {setDescriptionAction, setTitleAction} from './action/sync.action';

const initialState: BannerStateInterface = {
  title: null,
  description: null,
};

const bannerReducer = createReducer(
  initialState,
  on(
    setTitleAction,
    (state, action): BannerStateInterface => ({
      ...state,
      title: action.title,
    })
  ),
  on(
    setDescriptionAction,
    (state, action): BannerStateInterface => ({
      ...state,
      description: action.description,
    })
  ),
  on(routerNavigatedAction, (): BannerStateInterface => initialState)
);

export function reducers(state: BannerStateInterface, action: Action) {
  return bannerReducer(state, action);
}
