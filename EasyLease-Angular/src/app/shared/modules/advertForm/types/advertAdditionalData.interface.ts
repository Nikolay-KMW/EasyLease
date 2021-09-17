import {ComfortType} from 'src/app/shared/types/comfort.type';
import {AdvertLocation} from './advertLocation.interface';
import {PriceTypeExtended} from './priceTypeExtended.interface';

export interface AdvertAdditionalData {
  advertType: string[];
  settlementType: string[];
  streetType: string[];
  locations: AdvertLocation[];
  comforts: ComfortType[];
  priceType: PriceTypeExtended[];
}
