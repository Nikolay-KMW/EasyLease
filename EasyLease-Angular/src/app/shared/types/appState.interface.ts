import {AdvertStateInterface} from 'src/app/advert/types/advertState.interface';
import {AuthStateInterface} from 'src/app/auth/types/authState.interface';
import {CreateAdvertStateInterface} from 'src/app/createAdvert/types/createAdvertState.interface';
import {EditAdvertStateInterface} from 'src/app/editAdvert/types/editAdvertState.interface';
import {SettingsStateInterface} from 'src/app/settings/types/settingsState.interface';
import {UserProfileStateInterface} from 'src/app/userProfile/types/userProfileState.interface';
import {AdvertFormStateInterface} from '../modules/advertForm/types/advertFormState.interface';
import {BannerStateInterface} from '../modules/banner/types/bannerState.interface';
import {FeedStateInterface} from '../modules/feed/types/feedState.interface';
import {TagsStateInterface} from '../modules/tags/types/tagsState.interface';
import {TopBarStateInterface} from '../modules/topBar/types/topBarState.interface';

export interface AppStateInterface {
  auth: AuthStateInterface;
  topBar: TopBarStateInterface;
  banner: BannerStateInterface;
  feed: FeedStateInterface;
  tags: TagsStateInterface;
  advert: AdvertStateInterface;
  advertForm: AdvertFormStateInterface;
  createAdvert: CreateAdvertStateInterface;
  editAdvert: EditAdvertStateInterface;
  settings: SettingsStateInterface;
  userProfile: UserProfileStateInterface;
}
