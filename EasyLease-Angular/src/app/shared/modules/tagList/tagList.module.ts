import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatChipsModule} from '@angular/material/chips';

import {TagListComponent} from './components/tagList.component';

@NgModule({
  declarations: [TagListComponent],
  imports: [CommonModule, MatChipsModule],
  exports: [TagListComponent],
  providers: [],
})
export class TagListModule {}
