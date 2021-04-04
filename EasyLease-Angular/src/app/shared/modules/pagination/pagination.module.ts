import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PaginationComponent} from './components/pagination.component';
import {UtilsService} from '../../services/utils.service';
import {RouterModule} from '@angular/router';
import {MatButtonModule} from '@angular/material/button';

@NgModule({
  declarations: [PaginationComponent],
  imports: [CommonModule, RouterModule, MatButtonModule],
  exports: [PaginationComponent],
  providers: [UtilsService],
})
export class PaginationModule {}
