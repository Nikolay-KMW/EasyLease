import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatChipsModule} from '@angular/material/chips';

import {ComfortListComponent} from './components/comfortList/comfortList.component';

@NgModule({
  declarations: [ComfortListComponent],
  imports: [CommonModule, MatChipsModule],
  exports: [ComfortListComponent],
  providers: [],
})
export class ComfortListModule {}
