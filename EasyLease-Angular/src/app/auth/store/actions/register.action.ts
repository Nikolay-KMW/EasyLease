import {createAction, props} from '@ngrx/store';

import {ActionTypes} from '../actionTypes';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {CurrentUserInterface} from 'src/app/shared/types/currentUser.interface';
import {RegisterRequestInterface} from '../../types/registerRequest.interface';

export const registerAction = createAction(ActionTypes.REGISTER, props<{request: RegisterRequestInterface}>());

export const registerSuccessAction = createAction(
  ActionTypes.REGISTER_SUCCESS,
  props<{currentUser: CurrentUserInterface}>()
);

export const registerFailureAction = createAction(
  ActionTypes.REGISTER_FAILURE,
  props<{errors: BackendErrorInterface}>()
);
