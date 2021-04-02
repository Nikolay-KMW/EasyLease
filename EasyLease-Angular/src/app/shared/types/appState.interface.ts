import {AuthStateInterface} from 'src/app/auth/types/authState.interface';
import {FeedStateInterface} from '../modules/feed/types/feedState.interface';
import {TopBarStateInterface} from '../modules/topBar/types/topBarState.interface';

export interface AppStateInterface {
  auth: AuthStateInterface;
  topBar: TopBarStateInterface;
  feed: FeedStateInterface;
}
