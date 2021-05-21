import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';

import {FavoritedFeedComponent} from './components/favoritedFeed/favoritedFeed.component';
import {FeedModule} from '../shared/modules/feed/feed.module';
import {TagsModule} from '../shared/modules/tags/tags.module';

const routes = [{path: 'profile/:slug/favorites', component: FavoritedFeedComponent}];

@NgModule({
  declarations: [FavoritedFeedComponent],
  imports: [CommonModule, RouterModule.forChild(routes), FeedModule, TagsModule],
  exports: [],
  providers: [],
})
export class FavoritedFeedModule {}
