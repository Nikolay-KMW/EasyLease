import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';

import {FilterFeedComponent} from './components/filterFeed/filterFeed.component';
import {FeedModule} from '../shared/modules/feed/feed.module';
import {TagsModule} from '../shared/modules/tags/tags.module';

const routes = [{path: 'tags/:slug', component: FilterFeedComponent}];

@NgModule({
  declarations: [FilterFeedComponent],
  imports: [CommonModule, RouterModule.forChild(routes), FeedModule, TagsModule],
  exports: [],
  providers: [],
})
export class FilterFeedModule {}
