import {ComfortType} from './comfort.type';
import {ImageInterface} from './image.interface';
import {PriceType} from './price.type';
import {ProfileInterface} from './profile.Interface';
import {TagType} from './tag.type';

export interface AdvertInterface {
  id: string;
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
  images: ImageInterface[];
  priceType: PriceType;
  price: number;
  startOfLease: string;
  endOfLease: string | null;
  createdAd: string;
  updatedAd: string | null;
  slug: string;
  comfortList: ComfortType[];
  tagList: TagType[];
  favorited: boolean;
  author: ProfileInterface;
}
