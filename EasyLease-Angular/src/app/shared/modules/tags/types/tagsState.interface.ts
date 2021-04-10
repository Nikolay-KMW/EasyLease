import {TagType} from 'src/app/shared/types/Tag.type';

export interface TagsStateInterface {
  isLoading: boolean;
  error: string | null;
  date: TagType[] | null;
}
