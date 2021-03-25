import {Action, createReducer, on} from '@ngrx/store';

import {TopBarStateInterface} from '../types/topBarState.interface';
import {closeSidenavAction, openSidenavAction} from './actions/toggle.action';

const initialState: TopBarStateInterface = {
  isOpenedSidenav: false,
};

const topBarReducer = createReducer(
  initialState,
  on(
    openSidenavAction,
    (state): TopBarStateInterface => ({
      ...state,
      isOpenedSidenav: true,
    })
  ),
  on(
    closeSidenavAction,
    (state): TopBarStateInterface => ({
      ...state,
      isOpenedSidenav: false,
    })
  )
);

export function reducers(state: TopBarStateInterface, action: Action) {
  return topBarReducer(state, action);
}
