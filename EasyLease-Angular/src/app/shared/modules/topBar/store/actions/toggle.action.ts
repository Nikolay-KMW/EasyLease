import {createAction} from '@ngrx/store';

import {ActionTypes} from '../actionTypes';

export const openSidenavAction = createAction(ActionTypes.OPEN_SIDENAV);

export const closeSidenavAction = createAction(ActionTypes.CLOSE_SIDENAV);
