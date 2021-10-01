import {Action, createReducer, on} from '@ngrx/store';

import {UploadAdvertPhotoStateInterface} from '../types/uploadAdvertPhotoState.interface';
import {
  getPhotoForAdvertAction,
  getPhotoForAdvertFailureAction,
  getPhotoForAdvertSuccessAction,
  quantityDownloadPhotoAction,
} from './actions/getPhoto.action';
import {uploadPhotoAction, uploadPhotoFailureAction, uploadPhotoSuccessAction} from './actions/uploadPhoto.action';

const initialState: UploadAdvertPhotoStateInterface = {
  photos: [],
  quantityPhoto: 0,
  isLoading: false,
  isSubmitting: false,
  validationErrors: null,
  isFalling: false,
};

const uploadAdvertPhotoReducer = createReducer(
  initialState,
  on(
    getPhotoForAdvertAction,
    (state): UploadAdvertPhotoStateInterface => ({
      ...state,
      isLoading: true,
    })
  ),
  on(
    quantityDownloadPhotoAction,
    (state, action): UploadAdvertPhotoStateInterface => ({
      ...state,
      quantityPhoto: action.quantity,
    })
  ),
  on(
    getPhotoForAdvertSuccessAction,
    (state, action): UploadAdvertPhotoStateInterface => ({
      ...state,
      photos: action.photos,
      isLoading: false,
    })
  ),
  on(
    getPhotoForAdvertFailureAction,
    (state): UploadAdvertPhotoStateInterface => ({
      ...state,
      isLoading: false,
      isFalling: true,
    })
  ),
  on(
    uploadPhotoAction,
    (state): UploadAdvertPhotoStateInterface => ({
      ...state,
      isSubmitting: true,
    })
  ),
  on(
    uploadPhotoSuccessAction,
    (state): UploadAdvertPhotoStateInterface => ({
      ...state,
      isSubmitting: false,
    })
  ),
  on(
    uploadPhotoFailureAction,
    (state, action): UploadAdvertPhotoStateInterface => ({
      ...state,
      isSubmitting: false,
      validationErrors: action.errors,
    })
  )
);

export function reducers(state: UploadAdvertPhotoStateInterface, action: Action) {
  return uploadAdvertPhotoReducer(state, action);
}
