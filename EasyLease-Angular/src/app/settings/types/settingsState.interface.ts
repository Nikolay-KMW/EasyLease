import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';

export interface SettingsStateInterface {
  isSubmitting: boolean;
  validationErrors: BackendErrorInterface | null;
}
