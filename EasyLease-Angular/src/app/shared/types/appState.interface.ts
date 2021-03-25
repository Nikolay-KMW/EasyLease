import {AuthStateInterface} from 'src/app/auth/types/authState.interface';
import {TopBarStateInterface} from '../modules/topBar/types/topBarState.interface';

export interface AppStateInterface {
  auth: AuthStateInterface;
  topBar: TopBarStateInterface;
}
