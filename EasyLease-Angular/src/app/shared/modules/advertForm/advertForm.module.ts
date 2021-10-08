import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ReactiveFormsModule} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatStepperModule} from '@angular/material/stepper';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {MatChipsModule} from '@angular/material/chips';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatMomentDateModule} from '@angular/material-moment-adapter';
import {EffectsModule} from '@ngrx/effects';
import {StoreModule} from '@ngrx/store';
import {MatSnackBarModule} from '@angular/material/snack-bar';

import {AdvertFormComponent} from './components/advertForm/advertForm.component';
import {BackendErrorMessageModule} from '../backendErrorMessage/backendErrorMessage.module';
import {LoadingModule} from '../loading/loading.module';
import {ErrorMessageModule} from '../errorMessage/errorMessage.module';
import {GetAdditionalDataEffect} from './store/effects/getAdditionalData.effect';
import {reducers} from './store/reducers';
import {AdditionalDataService} from './services/AdditionalData.service';
import {MatListModule} from '@angular/material/list';
import {NgxDropzoneModule} from 'ngx-dropzone';

@NgModule({
  declarations: [AdvertFormComponent],
  imports: [
    CommonModule,
    EffectsModule.forFeature([GetAdditionalDataEffect]),
    StoreModule.forFeature('advertForm', reducers),
    ReactiveFormsModule,
    LoadingModule,
    ErrorMessageModule,
    MatAutocompleteModule,
    MatSelectModule,
    MatStepperModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatListModule,
    FontAwesomeModule,
    MatChipsModule,
    NgxDropzoneModule,
    BackendErrorMessageModule,
    MatSnackBarModule,
  ],
  exports: [AdvertFormComponent],
  providers: [AdditionalDataService],
})
export class AdvertFormModule {}
