import {Component, Input, OnInit} from '@angular/core';
import {IconDefinition} from '@fortawesome/fontawesome-svg-core';
import {faHeart} from '@fortawesome/free-solid-svg-icons';
import {Store} from '@ngrx/store';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {addToFavoritesAction} from '../../store/actions/addToFavorites.action';

@Component({
  selector: 'el-add-to-favorites',
  templateUrl: './addToFavorites.component.html',
  styleUrls: ['./addToFavorites.component.scss'],
})
export class AddToFavoritesComponent implements OnInit {
  @Input('isFavorited') isFavoritedProps: boolean = false;
  @Input('favoritesCount') favoritesCountProps: number = 0;
  @Input('advertSlug') advertSlugProps: string | null = null;

  favoritesCount: number = 0;
  isFavorited: boolean = false;

  faHeart: IconDefinition = faHeart;
  constructor(private store: Store<AppStateInterface>) {}

  ngOnInit(): void {
    this.favoritesCount = this.favoritesCountProps;
    this.isFavorited = this.isFavoritedProps;
  }

  handleLike(): void {
    if (this.advertSlugProps) {
      this.store.dispatch(addToFavoritesAction({isFavorited: this.isFavorited, slug: this.advertSlugProps}));
    }

    if (this.isFavorited) {
      this.favoritesCount = this.favoritesCount - 1;
    } else {
      this.favoritesCount = this.favoritesCount + 1;
    }

    this.isFavorited = !this.isFavorited;
  }
}
