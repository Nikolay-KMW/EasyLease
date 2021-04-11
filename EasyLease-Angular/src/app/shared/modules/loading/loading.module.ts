import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

import {LoadingComponent} from './components/loading.component';
@NgModule({
  declarations: [LoadingComponent],
  imports: [CommonModule, MatProgressBarModule, MatProgressSpinnerModule],
  exports: [LoadingComponent],
  providers: [],
})
export class LoadingModule {}
