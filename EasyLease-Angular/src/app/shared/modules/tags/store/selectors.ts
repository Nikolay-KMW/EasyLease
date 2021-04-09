import {createFeatureSelector, createSelector} from '@ngrx/store';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {TagsStateInterface} from '../types/tagsState.interface';

export const tagsFeatureSelector = createFeatureSelector<AppStateInterface, TagsStateInterface>('tags');

export const isLoadingSelector = createSelector(
  tagsFeatureSelector,
  (tagsState: TagsStateInterface) => tagsState.isLoading
);

export const errorSelector = createSelector(tagsFeatureSelector, (tagsState: TagsStateInterface) => tagsState.error);

export const tagsSelector = createSelector(tagsFeatureSelector, (tagsState: TagsStateInterface) => tagsState.date);
