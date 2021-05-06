import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {MatButtonModule} from '@angular/material/button';

import {UtilsService} from '../../services/utils.service';
import {PaginationComponent} from './components/pagination/pagination.component';

@NgModule({
  declarations: [PaginationComponent],
  imports: [CommonModule, RouterModule, MatButtonModule],
  exports: [PaginationComponent],
  providers: [UtilsService],
})
export class PaginationModule {}
