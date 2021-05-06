import {AdvertInterface} from 'src/app/shared/types/advert.interface';

export interface GetFeedResponseInterface {
  articles: AdvertInterface[];
  articlesCount: number;
}
