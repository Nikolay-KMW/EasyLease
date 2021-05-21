import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';

import {UserFeedComponent} from './components/userFeed/userFeed.component';
import {FeedModule} from '../shared/modules/feed/feed.module';
import {TagsModule} from '../shared/modules/tags/tags.module';

const routes = [{path: 'profile/:slug', component: UserFeedComponent}];

@NgModule({
  declarations: [UserFeedComponent],
  imports: [CommonModule, RouterModule.forChild(routes), FeedModule, TagsModule],
  exports: [],
  providers: [],
})
export class UserFeedModule {}
