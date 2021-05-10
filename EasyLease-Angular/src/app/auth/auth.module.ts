import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MatInputModule} from '@angular/material/input';
import {ReactiveFormsModule} from '@angular/forms';
import {StoreModule} from '@ngrx/store';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {EffectsModule} from '@ngrx/effects';
import {MatButtonModule} from '@angular/material/button';

//import {RegisterComponent} from './components/register/register.component';
import {RegisterComponent} from 'src/app/auth/components/register/register.component';
import {reducers} from './store/reducers';
import {AuthService} from './services/auth.service';
import {RegisterEffect} from './store/effects/register.effect';
import {BackendErrorMessageModule} from '../shared/modules/backendErrorMessage/backendErrorMessage.module';
import {PersistanceService} from '../shared/services/persistance.service';
import {LoginEffect} from './store/effects/login.effect';
import {LoginComponent} from './components/login/login.component';
import {GetCurrentUserEffect} from './store/effects/getCurrentUser.effect';
import {UpdateCurrentUserEffect} from './store/effects/updateCurrentUser.effect';

const routes: Routes = [
  {path: 'register', component: RegisterComponent},
  {path: 'login', component: LoginComponent},
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    StoreModule.forFeature('auth', reducers),
    EffectsModule.forFeature([RegisterEffect, LoginEffect, GetCurrentUserEffect, UpdateCurrentUserEffect]),
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    FontAwesomeModule,
    BackendErrorMessageModule,
  ],
  declarations: [RegisterComponent, LoginComponent],
  providers: [AuthService, PersistanceService],
})
export class AuthModule {}
