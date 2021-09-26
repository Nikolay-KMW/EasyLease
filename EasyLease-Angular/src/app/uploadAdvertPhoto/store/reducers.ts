import {Action, createReducer, on} from '@ngrx/store';

import {UploadAdvertPhotoStateInterface} from '../types/createAdvertState.interface';
import {
  getPhotoForAdvertAction,
  getPhotoForAdvertFailureAction,
  getPhotoForAdvertSuccessAction,
} from './actions/getPhoto.action';
import {uploadPhotoAction, uploadPhotoFailureAction, uploadPhotoSuccessAction} from './actions/uploadPhoto.action';

const initialState: UploadAdvertPhotoStateInterface = {
  photos: null,
  isLoading: false,
  isSubmitting: false,
  validationErrors: null,
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
    getPhotoForAdvertSuccessAction,
    (state, action): UploadAdvertPhotoStateInterface => ({
      ...state,
      photos: action.files,
      isLoading: false,
    })
  ),
  on(
    getPhotoForAdvertFailureAction,
    (state): UploadAdvertPhotoStateInterface => ({
      ...state,
      isLoading: false,
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
