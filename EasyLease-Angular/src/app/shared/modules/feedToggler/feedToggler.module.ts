import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatTabsModule} from '@angular/material/tabs';

import {FeedTogglerComponent} from './components/feedToggler/feedToggler.component';
import {RouterModule} from '@angular/router';

@NgModule({
  declarations: [FeedTogglerComponent],
  imports: [CommonModule, MatTabsModule, RouterModule],
  exports: [FeedTogglerComponent],
  providers: [],
})
export class FeedTogglerModule {}
