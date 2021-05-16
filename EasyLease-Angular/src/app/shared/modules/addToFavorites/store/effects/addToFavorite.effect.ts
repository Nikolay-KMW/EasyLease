import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, switchMap} from 'rxjs/operators';
import {of} from 'rxjs';

import {AddToFavoritesService} from '../../services/addToFavorites.service';
import {
  addToFavoritesAction,
  addToFavoritesFailureAction,
  addToFavoritesSuccessAction,
} from '../actions/addToFavorites.action';
import {AdvertInterface} from 'src/app/shared/types/advert.interface';

@Injectable()
export class AddToFavoritesEffect {
  constructor(private actions$: Actions, private addToFavoritesService: AddToFavoritesService) {}

  addToFavorites$ = createEffect(() =>
    this.actions$.pipe(
      ofType(addToFavoritesAction),
      switchMap(({isFavorited, slug}) => {
        const advert$ = isFavorited
          ? this.addToFavoritesService.removeFromFavorites(slug)
          : this.addToFavoritesService.addToFavorites(slug);

        return advert$.pipe(
          map((advert: AdvertInterface) => {
            return addToFavoritesSuccessAction({advert});
          }),
          catchError(() => {
            return of(addToFavoritesFailureAction());
          })
        );
      })
    )
  );
}
