import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap} from 'rxjs/operators';
import {of} from 'rxjs';

import {TagsService} from '../../services/tags.service';
import {getTagsAction, getTagsFailureAction, getTagsSuccessAction} from '../actions/getTags.action';
import {TagType} from 'src/app/shared/types/Tag.type';

@Injectable()
export class GetTagsEffect {
  constructor(private actions$: Actions, private tagsService: TagsService) {}

  getTags$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getTagsAction),
      switchMap(({url}) => {
        return this.tagsService.getTags(url).pipe(
          map((tags: TagType[]) => {
            return getTagsSuccessAction({tags});
          }),
          catchError(() => {
            return of(getTagsFailureAction());
          })
        );
      })
    )
  );
}
