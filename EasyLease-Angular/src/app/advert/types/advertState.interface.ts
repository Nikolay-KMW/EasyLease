import {AdvertInterface} from 'src/app/shared/types/advert.interface';

export interface AdvertStateInterface {
  isLoading: boolean;
  error: string | null;
  date: AdvertInterface | null;
}
