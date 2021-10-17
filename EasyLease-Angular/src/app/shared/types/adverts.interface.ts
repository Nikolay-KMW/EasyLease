import {ComfortType} from './comfort.type';
import {ImageInterface} from './image.interface';
import {PriceType} from './price.type';
import {TagType} from './tag.type';

export interface AdvertsInterface {
  id: string;
  realtyType: string;
  title: string;
  description: string;
  fullAddress: string;
  image: ImageInterface | null;
  createdAd: string;
  slug: string;
  priceType: PriceType;
  price: number;
  comfortList: ComfortType[];
  tagList: TagType[];
  favorited: boolean;
}
