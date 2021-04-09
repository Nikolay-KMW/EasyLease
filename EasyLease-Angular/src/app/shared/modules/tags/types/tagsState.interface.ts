import {GetTagsResponseInterface} from './getTagsResponse.interface';

export interface TagsStateInterface {
  isLoading: boolean;
  error: string | null;
  date: GetTagsResponseInterface | null;
}
