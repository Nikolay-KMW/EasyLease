import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';

export interface CreateAdvertStateInterface {
  isSubmitting: boolean;
  validationErrors: BackendErrorInterface | null;
}
