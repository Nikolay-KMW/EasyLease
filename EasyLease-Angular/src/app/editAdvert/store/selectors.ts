import {createFeatureSelector, createSelector} from '@ngrx/store';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {EditAdvertStateInterface} from '../types/editAdvertState.interface';

export const editAdvertFeatureSelector = createFeatureSelector<AppStateInterface, EditAdvertStateInterface>(
  'editAdvert'
);

export const advertSelector = createSelector(
  editAdvertFeatureSelector,
  (editAdvertState: EditAdvertStateInterface) => editAdvertState.advert
);

export const isLoadingSelector = createSelector(
  editAdvertFeatureSelector,
  (editAdvertState: EditAdvertStateInterface) => editAdvertState.isLoading
);

export const isSubmittingSelector = createSelector(
  editAdvertFeatureSelector,
  (editAdvertState: EditAdvertStateInterface) => editAdvertState.isSubmitting
);

export const validationErrorsSelector = createSelector(
  editAdvertFeatureSelector,
  (editAdvertState: EditAdvertStateInterface) => editAdvertState.validationErrors
);
