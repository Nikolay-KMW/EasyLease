import {AdvertStateInterface} from 'src/app/advert/types/advertState.interface';
import {AuthStateInterface} from 'src/app/auth/types/authState.interface';
import {CreateAdvertStateInterface} from 'src/app/createAdvert/types/createAdvertState.interface';
import {EditAdvertStateInterface} from 'src/app/editAdvert/types/editAdvertState.interface';
import {FeedStateInterface} from '../modules/feed/types/feedState.interface';
import {TagsStateInterface} from '../modules/tags/types/tagsState.interface';
import {TopBarStateInterface} from '../modules/topBar/types/topBarState.interface';

export interface AppStateInterface {
  auth: AuthStateInterface;
  topBar: TopBarStateInterface;
  feed: FeedStateInterface;
  tags: TagsStateInterface;
  advert: AdvertStateInterface;
  createAdvert: CreateAdvertStateInterface;
  editAdvert: EditAdvertStateInterface;
}
