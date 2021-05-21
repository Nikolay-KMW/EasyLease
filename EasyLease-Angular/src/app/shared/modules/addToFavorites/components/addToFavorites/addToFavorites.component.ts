import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {IconDefinition} from '@fortawesome/fontawesome-svg-core';
import {faHeart} from '@fortawesome/free-solid-svg-icons';
import {select, Store} from '@ngrx/store';
import {Subscription} from 'rxjs';
import {isLoggedInSelector} from 'src/app/auth/store/selectors';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {addToFavoritesAction, addToFavoritesFailureAction} from '../../store/actions/addToFavorites.action';

@Component({
  selector: 'el-add-to-favorites',
  templateUrl: './addToFavorites.component.html',
  styleUrls: ['./addToFavorites.component.scss'],
})
export class AddToFavoritesComponent implements OnInit, OnDestroy {
  @Input('isFavorited') isFavoritedProps: boolean = false;
  @Input('advertSlug') advertSlugProps: string | null = null;

  isLoggedInSubscription: Subscription;
  isLoggedIn: boolean = false;
  isFavorited: boolean = false;

  faHeart: IconDefinition = faHeart;
  constructor(private store: Store<AppStateInterface>) {
    this.isLoggedInSubscription = this.store.pipe(select(isLoggedInSelector)).subscribe((isLoggedIn) => {
      isLoggedIn == null ? (this.isLoggedIn = false) : (this.isLoggedIn = isLoggedIn);
    });
  }

  ngOnInit(): void {
    this.isFavorited = this.isFavoritedProps;
  }

  handleLike(): void {
    if (this.advertSlugProps && this.isLoggedIn) {
      this.store.dispatch(addToFavoritesAction({isFavorited: this.isFavorited, slug: this.advertSlugProps}));
      this.isFavorited = !this.isFavorited;
    } else {
      this.store.dispatch(addToFavoritesFailureAction());
    }
  }

  ngOnDestroy(): void {
    this.isLoggedInSubscription.unsubscribe();
  }
}
