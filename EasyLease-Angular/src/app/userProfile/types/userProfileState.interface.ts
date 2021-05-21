import {ProfileInterface} from 'src/app/shared/types/profile.Interface';

export interface UserProfileStateInterface {
  date: ProfileInterface | null;
  isLoading: boolean;
  error: string | null;
}
