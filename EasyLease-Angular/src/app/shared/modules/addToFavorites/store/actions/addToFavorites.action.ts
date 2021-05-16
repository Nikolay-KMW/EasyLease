import {createAction, props} from '@ngrx/store';
import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {ActionTypes} from '../actionTypes';

export const addToFavoritesAction = createAction(
  ActionTypes.ADD_TO_FAVORITES,
  props<{isFavorited: boolean; slug: string}>()
);

export const addToFavoritesSuccessAction = createAction(
  ActionTypes.ADD_TO_FAVORITES_SUCCESS,
  props<{advert: AdvertInterface}>()
);

export const addToFavoritesFailureAction = createAction(ActionTypes.ADD_TO_FAVORITES_FAILURE);
