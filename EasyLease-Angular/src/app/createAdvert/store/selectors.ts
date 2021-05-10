import {createFeatureSelector, createSelector} from '@ngrx/store';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {CreateAdvertStateInterface} from '../types/createAdvertState.interface';

export const createAdvertFeatureSelector = createFeatureSelector<AppStateInterface, CreateAdvertStateInterface>(
  'createAdvert'
);

export const isSubmittingSelector = createSelector(
  createAdvertFeatureSelector,
  (createAdvertState: CreateAdvertStateInterface) => createAdvertState.isSubmitting
);

export const validationErrorsSelector = createSelector(
  createAdvertFeatureSelector,
  (createAdvertState: CreateAdvertStateInterface) => createAdvertState.validationErrors
);
