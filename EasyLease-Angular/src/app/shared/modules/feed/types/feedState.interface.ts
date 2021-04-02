import {GetFeedResponseInterface} from './getFeedResponse.interface';

export interface FeedStateInterface {
  isLoading: boolean;
  error: string | null;
  date: GetFeedResponseInterface | null;
}
