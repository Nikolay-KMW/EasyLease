import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {SettingsComponent} from './components/settings/settings.component';
import {RouterModule} from '@angular/router';
import {StoreModule} from '@ngrx/store';
import {reducers} from './store/reducers';
import {ReactiveFormsModule} from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {BackendErrorMessageModule} from '../shared/modules/backendErrorMessage/backendErrorMessage.module';

const routs = [
  {
    path: 'profile/settings',
    component: SettingsComponent,
  },
];

@NgModule({
  declarations: [SettingsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routs),
    StoreModule.forFeature('settings', reducers),
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    FontAwesomeModule,
    BackendErrorMessageModule,
  ],
  exports: [],
  providers: [],
})
export class SettingsModule {}
