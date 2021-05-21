import {routerNavigatedAction} from '@ngrx/router-store';
import {Action, createReducer, on} from '@ngrx/store';

import {UserProfileStateInterface} from '../types/userProfileState.interface';
import {
  getUserProfileAction,
  getUserProfileFailureAction,
  getUserProfileSuccessAction,
} from './action/getUserProfile.action';

const initialState: UserProfileStateInterface = {
  date: null,
  isLoading: false,
  error: null,
};

const UserProfileReducer = createReducer(
  initialState,
  on(
    getUserProfileAction,
    (state): UserProfileStateInterface => ({
      ...state,
      isLoading: true,
      error: null,
      date: null,
    })
  ),
  on(
    getUserProfileSuccessAction,
    (state, action): UserProfileStateInterface => ({
      ...state,
      isLoading: false,
      date: action.userProfile,
      error: null,
    })
  ),
  on(
    getUserProfileFailureAction,
    (state): UserProfileStateInterface => ({
      ...state,
      isLoading: false,
      error: 'error',
      date: null,
    })
  )
);

export function reducers(state: UserProfileStateInterface, action: Action) {
  return UserProfileReducer(state, action);
}
