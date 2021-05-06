import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatDividerModule} from '@angular/material/divider';

import {BannerComponent} from './components/banner/banner.component';

@NgModule({
  declarations: [BannerComponent],
  imports: [CommonModule, MatDividerModule],
  exports: [BannerComponent],
  providers: [],
})
export class BannerModule {}
