import {ComfortType} from './comfort.type';
import {PriceType} from './price.type';
import {TagType} from './tag.type';

export interface AdvertInputInterface {
  realtyType: string;
  title: string;
  description: string;
  numberOfRooms: number;
  area: number;
  storey: number | null;
  numberOfStoreys: number | null;
  region: string;
  district: string;
  settlementType: string;
  settlementName: string;
  streetType: string;
  streetName: string;
  houseNumber: string | null;
  apartmentNumber: number | null;
  priceType: PriceType;
  price: number;
  startOfLease: string;
  endOfLease: string | null;
  comfortList: ComfortType[];
  tagList: TagType[];
}
