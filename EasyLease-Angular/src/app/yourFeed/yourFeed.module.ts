import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';

import {YourFeedComponent} from './components/yourFeed/yourFeed.component';
import {FeedModule} from '../shared/modules/feed/feed.module';
import {BannerModule} from '../shared/modules/banner/banner.module';
import {TagsModule} from '../shared/modules/tags/tags.module';
import {FeedTogglerModule} from '../shared/modules/feedToggler/feedToggler.module';

const routes = [{path: 'feed', component: YourFeedComponent}];

@NgModule({
  declarations: [YourFeedComponent],
  imports: [CommonModule, RouterModule.forChild(routes), FeedModule, BannerModule, TagsModule, FeedTogglerModule],
  exports: [],
  providers: [],
})
export class YourFeedModule {}
