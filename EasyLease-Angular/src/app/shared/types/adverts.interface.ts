import {ComfortType} from './comfort.type';
import {ImagePathType} from './imagePath.type';
import {PriceType} from './price.type';
import {ProfileInterface} from './profile.Interface';
import {TagType} from './tag.type';

export interface AdvertsInterface {
  id: string;
  advertType: string;
  title: string;
  description: string;
  fullAddress: string;
  image: ImagePathType | null;
  createdAd: string;
  slug: string;
  priceType: PriceType;
  price: number;
  comfortList: ComfortType[];
  tagList: TagType[];
  favorited: boolean;
  // author: ProfileInterface | null; // Deleted
}
