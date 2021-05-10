import {Action, createReducer, on} from '@ngrx/store';

import {createAdvertAction, createAdvertSuccessAction, createAdvertFailureAction} from './actions/createAdvert.action';
import {CreateAdvertStateInterface} from '../types/createAdvertState.interface';

const initialState: CreateAdvertStateInterface = {
  isSubmitting: false,
  validationErrors: null,
};

const createAdvertReducer = createReducer(
  initialState,
  on(
    createAdvertAction,
    (state): CreateAdvertStateInterface => ({
      ...state,
      isSubmitting: true,
    })
  ),
  on(
    createAdvertSuccessAction,
    (state): CreateAdvertStateInterface => ({
      ...state,
      isSubmitting: false,
    })
  ),
  on(
    createAdvertFailureAction,
    (state, action): CreateAdvertStateInterface => ({
      ...state,
      isSubmitting: false,
      validationErrors: action.errors,
    })
  )
);

export function reducers(state: CreateAdvertStateInterface, action: Action) {
  return createAdvertReducer(state, action);
}
