import {createFeatureSelector, createSelector} from '@ngrx/store';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {AuthStateInterface} from '../types/authState.interface';

export const authFeatureSelector = createFeatureSelector<AppStateInterface, AuthStateInterface>('auth');

export const isSubmittingSelector = createSelector(
  authFeatureSelector,
  (authState: AuthStateInterface) => authState.isSubmitting
);

export const validationErrorsSelected = createSelector(
  authFeatureSelector,
  (authState: AuthStateInterface) => authState.validationErrors
);

export const isLoggedInSelected = createSelector(
  authFeatureSelector,
  (authState: AuthStateInterface) => authState.isLoggedIn
);

export const isAnonymousSelected = createSelector(
  authFeatureSelector,
  (authState: AuthStateInterface) => authState.isLoggedIn === false
);

export const currentUserSelected = createSelector(
  authFeatureSelector,
  (authState: AuthStateInterface) => authState.currentUser
);
