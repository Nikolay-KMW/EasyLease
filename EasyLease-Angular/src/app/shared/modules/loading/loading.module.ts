import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {LoadingComponent} from './components/loading.component';
import {MatProgressBarModule} from '@angular/material/progress-bar';

@NgModule({
  declarations: [LoadingComponent],
  imports: [CommonModule, MatProgressBarModule],
  exports: [LoadingComponent],
  providers: [],
})
export class LoadingModule {}
