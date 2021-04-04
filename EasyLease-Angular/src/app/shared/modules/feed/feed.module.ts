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

@NgModule({
  declarations: [FeedComponent],
  imports: [
    CommonModule,
    RouterModule,
    MatDividerModule,
    MatButtonModule,
    ErrorMessageModule,
    LoadingModule,
    PaginationModule,
    EffectsModule.forFeature([GetFeedEffect]),
    StoreModule.forFeature('feed', reducers),
  ],
  exports: [FeedComponent],
  providers: [FeedService],
})
export class FeedModule {}
