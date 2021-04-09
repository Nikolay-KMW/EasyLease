import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap} from 'rxjs/operators';
import {of} from 'rxjs';

import {TagsService} from '../../services/tags.service';
import {GetTagsResponseInterface} from '../../types/getTagsResponse.interface';
import {getTagsAction, getTagsFailureAction, getTagsSuccessAction} from '../actions/getTags.action';

@Injectable()
export class GetTagsEffect {
  constructor(private actions$: Actions, private tagsService: TagsService) {}

  getTags$ = createEffect(() =>
    this.actions$.pipe(
      ofType(getTagsAction),
      switchMap(({url}) => {
        return this.tagsService.getTags(url).pipe(
          map((tags: GetTagsResponseInterface) => {
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
