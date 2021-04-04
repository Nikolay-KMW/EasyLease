import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ErrorMessageComponent} from './components/errorMessage.component';

@NgModule({
  declarations: [ErrorMessageComponent],
  imports: [CommonModule],
  exports: [ErrorMessageComponent],
  providers: [],
})
export class ErrorMessageModule {}
