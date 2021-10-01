import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';

export interface UploadAdvertPhotoStateInterface {
  photos: File[];
  quantityPhoto: number;
  isLoading: boolean;
  isSubmitting: boolean;
  validationErrors: BackendErrorInterface | null;
  isFalling: boolean;
}
