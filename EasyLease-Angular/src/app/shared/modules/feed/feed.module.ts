import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {EffectsModule} from '@ngrx/effects';
import {StoreModule} from '@ngrx/store';
import {RouterModule} from '@angular/router';
import {MatDividerModule} from '@angular/material/divider';

import {FeedComponent} from './components/feed/feed.component';
import {GetFeedEffect} from './store/effects/getFeed.effect';
import {reducers} from './store/reducers';
import {FeedService} from './services/feed.service';
import {MatButtonModule} from '@angular/material/button';
import {ErrorMessageModule} from '../errorMessage/errorMessage.module';
import {LoadingModule} from '../loading/loading.module';
import {PaginationModule} from '../pagination/pagination.module';
import {TagListModule} from '../tagList/tagList.module';
import {AddToFavoritesModule} from '../addToFavorites/addToFavorites.module';
import {ComfortListModule} from '../comfortList/comfortList.module';

@NgModule({
  declarations: [FeedComponent],
  imports: [
    CommonModule,
    RouterModule,
    EffectsModule.forFeature([GetFeedEffect]),
    StoreModule.forFeature('feed', reducers),
    MatDividerModule,
    MatButtonModule,
    ErrorMessageModule,
    LoadingModule,
    PaginationModule,
    ComfortListModule,
    AddToFavoritesModule,
  ],
  exports: [FeedComponent],
  providers: [FeedService],
})
export class FeedModule {}
