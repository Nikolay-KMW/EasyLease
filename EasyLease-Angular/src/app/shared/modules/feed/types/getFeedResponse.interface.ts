import {AdvertsInterface} from 'src/app/shared/types/adverts.interface';

export interface GetFeedResponseInterface {
  adverts: AdvertsInterface[];
  advertCount: number;
}
