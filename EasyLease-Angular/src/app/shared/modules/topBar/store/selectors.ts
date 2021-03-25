import {createFeatureSelector, createSelector} from '@ngrx/store';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {TopBarStateInterface} from '../types/topBarState.interface';

export const topBarFeatureSelector = createFeatureSelector<AppStateInterface, TopBarStateInterface>('topBar');

export const isOpenedSidenavSelector = createSelector(
  topBarFeatureSelector,
  (topBarState: TopBarStateInterface) => topBarState.isOpenedSidenav
);
