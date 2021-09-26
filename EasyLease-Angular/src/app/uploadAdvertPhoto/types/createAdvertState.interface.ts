import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';

export interface UploadAdvertPhotoStateInterface {
  photos: File[] | null;
  isLoading: boolean;
  isSubmitting: boolean;
  validationErrors: BackendErrorInterface | null;
}
