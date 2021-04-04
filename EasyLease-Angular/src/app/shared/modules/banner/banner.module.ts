import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {BannerComponent} from './components/banner.component';
import {MatDividerModule} from '@angular/material/divider';

@NgModule({
  declarations: [BannerComponent],
  imports: [CommonModule, MatDividerModule],
  exports: [BannerComponent],
  providers: [],
})
export class BannerModule {}
