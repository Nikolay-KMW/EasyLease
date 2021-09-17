import {Action, createReducer, on} from '@ngrx/store';

import {
  getAdditionalDataAction,
  getAdditionalDataFailureAction,
  getAdditionalDataSuccessAction,
} from './actions/getAdditionalData.action';
import {AdvertFormStateInterface} from '../types/advertFormState.interface';

const initialState: AdvertFormStateInterface = {
  additionalData: null,
  isLoading: false,
  isFalling: false,
};

const advertFormReducer = createReducer(
  initialState,
  on(
    getAdditionalDataAction,
    (state): AdvertFormStateInterface => ({
      ...state,
      isLoading: true,
    })
  ),
  on(
    getAdditionalDataSuccessAction,
    (state, action): AdvertFormStateInterface => ({
      ...state,
      isLoading: false,
      additionalData: action.additionalData,
    })
  ),
  on(
    getAdditionalDataFailureAction,
    (state): AdvertFormStateInterface => ({
      ...state,
      isLoading: false,
      isFalling: true,
    })
  )
);

export function reducers(state: AdvertFormStateInterface, action: Action) {
  return advertFormReducer(state, action);
}
