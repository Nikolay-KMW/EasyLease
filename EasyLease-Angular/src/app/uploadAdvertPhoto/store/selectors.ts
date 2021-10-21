import {createFeatureSelector, createSelector} from '@ngrx/store';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {UploadAdvertPhotoStateInterface} from '../types/uploadAdvertPhotoState.interface';

export const uploadAdvertPhotoFeatureSelector = createFeatureSelector<
  AppStateInterface,
  UploadAdvertPhotoStateInterface
>('uploadAdvertPhoto');

export const photosSelector = createSelector(
  uploadAdvertPhotoFeatureSelector,
  (uploadAdvertPhotoState: UploadAdvertPhotoStateInterface) => uploadAdvertPhotoState.photos
);

export const isLoadingSelector = createSelector(
  uploadAdvertPhotoFeatureSelector,
  (uploadAdvertPhotoState: UploadAdvertPhotoStateInterface) => uploadAdvertPhotoState.isLoading
);

export const isSubmittingSelector = createSelector(
  uploadAdvertPhotoFeatureSelector,
  (uploadAdvertPhotoState: UploadAdvertPhotoStateInterface) => uploadAdvertPhotoState.isSubmitting
);

export const isDeletingSelector = createSelector(
  uploadAdvertPhotoFeatureSelector,
  (uploadAdvertPhotoState: UploadAdvertPhotoStateInterface) => uploadAdvertPhotoState.isDeleting
);

export const validationErrorsSelector = createSelector(
  uploadAdvertPhotoFeatureSelector,
  (uploadAdvertPhotoState: UploadAdvertPhotoStateInterface) => uploadAdvertPhotoState.validationErrors
);

export const isFallingSelector = createSelector(
  uploadAdvertPhotoFeatureSelector,
  (uploadAdvertPhotoState: UploadAdvertPhotoStateInterface) => uploadAdvertPhotoState.isFalling
);
