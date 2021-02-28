import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {BackendErrorMessageComponent} from './components/backendErrorMessage/backendErrorMessage.component';
import {MatInputModule} from '@angular/material/input';

@NgModule({
  declarations: [BackendErrorMessageComponent],
  imports: [CommonModule, MatInputModule],
  exports: [BackendErrorMessageComponent],
  providers: [],
})
export class BackendErrorMessageModule {}
