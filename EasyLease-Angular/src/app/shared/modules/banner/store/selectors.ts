import {createFeatureSelector, createSelector} from '@ngrx/store';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {BannerStateInterface} from '../types/bannerState.interface';

export const BannerFeatureSelector = createFeatureSelector<AppStateInterface, BannerStateInterface>('banner');

export const titleSelector = createSelector(
  BannerFeatureSelector,
  (bannerState: BannerStateInterface) => bannerState.title
);

export const descriptionSelector = createSelector(
  BannerFeatureSelector,
  (bannerState: BannerStateInterface) => bannerState.description
);
