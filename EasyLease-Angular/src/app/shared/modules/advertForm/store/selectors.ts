import {createFeatureSelector, createSelector} from '@ngrx/store';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {AdvertFormStateInterface} from '../types/advertFormState.interface';

export const advertFormFeatureSelector = createFeatureSelector<AppStateInterface, AdvertFormStateInterface>(
  'advertForm'
);

export const additionalDataSelector = createSelector(
  advertFormFeatureSelector,
  (advertFormState: AdvertFormStateInterface) => advertFormState.additionalData
);

export const isLoadingSelector = createSelector(
  advertFormFeatureSelector,
  (advertFormState: AdvertFormStateInterface) => advertFormState.isLoading
);

export const isFallingSelector = createSelector(
  advertFormFeatureSelector,
  (advertFormState: AdvertFormStateInterface) => advertFormState.isFalling
);
