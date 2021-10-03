import {createFeatureSelector, createSelector} from '@ngrx/store';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {AdvertStateInterface} from '../types/advertState.interface';

export const advertFeatureSelector = createFeatureSelector<AppStateInterface, AdvertStateInterface>('advert');

export const isLoadingSelector = createSelector(
  advertFeatureSelector,
  (advertState: AdvertStateInterface) => advertState.isLoading
);

export const isFallingSelector = createSelector(
  advertFeatureSelector,
  (advertState: AdvertStateInterface) => advertState.isFalling
);

export const errorSelector = createSelector(
  advertFeatureSelector,
  (advertState: AdvertStateInterface) => advertState.error
);

export const advertSelector = createSelector(
  advertFeatureSelector,
  (advertState: AdvertStateInterface) => advertState.date
);
