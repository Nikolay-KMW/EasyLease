import {LOCALE_ID, NgModule} from '@angular/core';
import {CommonModule, DatePipe, formatDate, registerLocaleData} from '@angular/common';
import {ReactiveFormsModule} from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatStepperModule} from '@angular/material/stepper';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {MatChipsModule} from '@angular/material/chips';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {
  DateAdapter,
  MatDateFormats,
  MatNativeDateModule,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE,
  NativeDateAdapter,
} from '@angular/material/core';
import {EffectsModule} from '@ngrx/effects';
import {StoreModule} from '@ngrx/store';
import ru from '@angular/common/locales/ru';

import {AdvertFormComponent} from './components/advertForm/advertForm.component';
import {BackendErrorMessageModule} from '../backendErrorMessage/backendErrorMessage.module';
import {LoadingModule} from '../loading/loading.module';
import {ErrorMessageModule} from '../errorMessage/errorMessage.module';
import {GetAdditionalDataEffect} from './store/effects/getAdditionalData.effect';
import {reducers} from './store/reducers';
import {AdditionalDataService} from './services/AdditionalData.service';

registerLocaleData(ru);

// export const DATE_FORMATS: MatDateFormats = {
//   parse: {
//     dateInput: 'DD.MM.YYYY',
//   },
//   display: {
//     dateInput: 'DD.MM.YYYY',
//     monthYearLabel: 'MMM YYYY',
//     dateA11yLabel: 'L',
//     monthYearA11yLabel: 'MMMM YYYY',
//   },
// };

// export const DATE_FORMATS = {
//   parse: {
//     dateInput: 'DD-MM-YYYY',
//   },
//   display: {
//     dateInput: 'MMM DD, YYYY',
//     monthYearLabel: 'MMMM YYYY',
//     dateA11yLabel: 'LL',
//     monthYearA11yLabel: 'MMMM YYYY',
//   },
// };

// export const DATE_FORMATS: MatDateFormats = {
//   parse: {
//     dateInput: {month: 'numeric', year: 'numeric', day: 'numeric'},
//   },
//   display: {
//     dateInput: 'input',
//     monthYearLabel: {year: 'numeric', month: 'numeric'},
//     dateA11yLabel: {year: 'numeric', month: 'long', day: 'numeric'},
//     monthYearA11yLabel: {year: 'numeric', month: 'long'},
//   },
// };

class AppDateAdapter extends NativeDateAdapter {
  // format(date: Date, displayFormat: Object): string {
  //   if (displayFormat === 'input') {
  //     let day: string = date.getDate().toString();
  //     day = +day < 10 ? '0' + day : day;
  //     let month: string = (date.getMonth() + 1).toString();
  //     month = +month < 10 ? '0' + month : month;
  //     const year: number = date.getFullYear();
  //     return `${day}.${month}.${year}`;
  //   }
  //   return date.toDateString();
  // }

  format(date: Date, displayFormat: Object): string {
    if (displayFormat === 'input') {
      //return formatDate(date, 'dd-MMM-yyyy', this.locale);
      // const transformDate = new DatePipe(this.locale).transform(date, 'EEE, MMM dd, yyyy');
      const transformDate = new DatePipe(this.locale).transform(date, 'EEE, MMM dd, yyyy', this.locale);
      return transformDate ? transformDate : date.toDateString();
    } else {
      return date.toDateString();
    }
  }

  parse(value: any): Date | null {
    if (typeof value === 'string' && value.indexOf(' ') > -1) {
      const str = value.split(' ');
      console.log(str);

      const year = Number(str[2]);
      const month = Number(str[1]) - 1;
      const date = Number(str[0]);

      return new Date(year, month, date);
    }
    const timestamp = typeof value === 'number' ? value : Date.parse(value);
    return isNaN(timestamp) ? null : new Date(timestamp);

    // Date.parse('EEE, MMM dd, yyyy', value)
    //      const transformDate = new DatePipe(this.locale).transform(value, 'EEE, MMM dd, yyyy');

    // const date = moment(value, environment.APP_DATE_FORMAT);
    // return date.isValid() ? date.toDate() : null;
  }
}

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
    MatNativeDateModule,
    FontAwesomeModule,
    MatChipsModule,
    BackendErrorMessageModule,
  ],
  exports: [AdvertFormComponent],
  providers: [
    AdditionalDataService,
    //{provide: LOCALE_ID, useValue: 'ru'},
    {provide: MAT_DATE_LOCALE, useValue: 'ru-RU'},
    //{provide: DateAdapter, useClass: AppDateAdapter},
    //{provide: MAT_DATE_FORMATS, useValue: DATE_FORMATS},
  ],
})
export class AdvertFormModule {}
