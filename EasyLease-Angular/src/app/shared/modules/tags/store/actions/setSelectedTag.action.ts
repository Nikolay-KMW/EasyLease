import {createAction, props} from '@ngrx/store';

import {TagType} from 'src/app/shared/types/Tag.type';
import {ActionTypes} from '../actionTypes';

export const setSelectedTagAction = createAction(ActionTypes.SET_SELECTED_TAG, props<{selectedTag: TagType}>());
