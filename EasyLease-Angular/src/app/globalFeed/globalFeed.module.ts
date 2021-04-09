import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';

import {GlobalFeedComponent} from './components/globalFeed/globalFeed.component';
import {FeedModule} from '../shared/modules/feed/feed.module';
import {BannerModule} from '../shared/modules/banner/banner.module';
import {TagsModule} from '../shared/modules/tags/tags.module';

const routes = [{path: '', component: GlobalFeedComponent}];

@NgModule({
  declarations: [GlobalFeedComponent],
  imports: [CommonModule, RouterModule.forChild(routes), FeedModule, BannerModule, TagsModule],
  exports: [],
  providers: [],
})
export class GlobalFeedModule {}
