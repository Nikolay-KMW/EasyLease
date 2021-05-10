import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';

export interface EditAdvertStateInterface {
  advert: AdvertInterface | null;
  isLoading: boolean;
  isSubmitting: boolean;
  validationErrors: BackendErrorInterface | null;
}
