import {AdvertAdditionalData} from './advertAdditionalData.interface';

export interface AdvertFormStateInterface {
  additionalData: AdvertAdditionalData | null;
  isLoading: boolean;
  isFalling: boolean;
}
