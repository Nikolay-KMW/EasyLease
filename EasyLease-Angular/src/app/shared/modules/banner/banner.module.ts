import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatDividerModule} from '@angular/material/divider';

import {BannerComponent} from './components/banner/banner.component';
import {StoreModule} from '@ngrx/store';
import {reducers} from './store/reducer';

@NgModule({
  declarations: [BannerComponent],
  imports: [CommonModule, MatDividerModule, StoreModule.forFeature('banner', reducers)],
  exports: [BannerComponent],
  providers: [],
})
export class BannerModule {}
