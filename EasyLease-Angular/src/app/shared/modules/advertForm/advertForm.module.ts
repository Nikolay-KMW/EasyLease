import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ReactiveFormsModule} from '@angular/forms';
import {MatStepperModule} from '@angular/material/stepper';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {MatChipsModule} from '@angular/material/chips';

import {AdvertFormComponent} from './components/advertForm/advertForm.component';
import {BackendErrorMessageModule} from '../backendErrorMessage/backendErrorMessage.module';

@NgModule({
  declarations: [AdvertFormComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatInputModule,
    MatButtonModule,
    FontAwesomeModule,
    MatChipsModule,
    BackendErrorMessageModule,
  ],
  exports: [AdvertFormComponent],
  providers: [],
})
export class AdvertFormModule {}
