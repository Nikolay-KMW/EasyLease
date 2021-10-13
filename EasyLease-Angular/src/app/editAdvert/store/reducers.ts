import {routerNavigatedAction} from '@ngrx/router-store';
import {Action, createReducer, on} from '@ngrx/store';

import {EditAdvertStateInterface} from '../types/editAdvertState.interface';
import {getAdvertAction, getAdvertFailureAction, getAdvertSuccessAction} from './actions/getAdvert.action';
import {updateAdvertAction, updateAdvertFailureAction, updateAdvertSuccessAction} from './actions/updateAdvert.action';

const initialState: EditAdvertStateInterface = {
  advert: null,
  isLoading: false,
  isSubmitting: false,
  validationErrors: null,
};

const editAdvertReducer = createReducer(
  initialState,
  on(
    getAdvertAction,
    (state): EditAdvertStateInterface => ({
      ...state,
      isLoading: true,
    })
  ),
  on(
    getAdvertSuccessAction,
    (state, action): EditAdvertStateInterface => ({
      ...state,
      advert: action.advert,
      isLoading: false,
    })
  ),
  on(
    getAdvertFailureAction,
    (state): EditAdvertStateInterface => ({
      ...state,
      isLoading: false,
    })
  ),
  on(
    updateAdvertAction,
    (state): EditAdvertStateInterface => ({
      ...state,
      validationErrors: null,
      isSubmitting: true,
    })
  ),
  on(
    updateAdvertSuccessAction,
    (state): EditAdvertStateInterface => ({
      ...state,
      isSubmitting: false,
    })
  ),
  on(
    updateAdvertFailureAction,
    (state, action): EditAdvertStateInterface => ({
      ...state,
      isSubmitting: false,
      validationErrors: action.errors,
    })
  ),
  on(
    routerNavigatedAction,
    (state): EditAdvertStateInterface => ({
      ...state,
      advert: null,
      isSubmitting: false,
      validationErrors: null,
    })
  )
);

export function reducers(state: EditAdvertStateInterface, action: Action) {
  return editAdvertReducer(state, action);
}
