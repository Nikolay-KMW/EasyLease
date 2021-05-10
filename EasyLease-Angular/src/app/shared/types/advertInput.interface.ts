import {TagType} from './Tag.type';

export interface AdvertInputInterface {
  title: string;
  description: string;
  body: string;
  tagList: TagType[] | null;
}
