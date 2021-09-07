import {TagType} from 'src/app/shared/types/tag.type';

export interface TagsStateInterface {
  isLoading: boolean;
  error: string | null;
  date: TagType[] | null;
  selectedTag: TagType | null;
}
