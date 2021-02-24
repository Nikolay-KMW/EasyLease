import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MatInputModule} from '@angular/material/input';
import {ReactiveFormsModule} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {StoreModule} from '@ngrx/store';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {EffectsModule} from '@ngrx/effects';

//import {RegisterComponent} from './components/register/register.component';
import {RegisterComponent} from 'src/app/auth/components/register/register.component';
import {reducers} from './store/reducers';
import {AuthService} from './services/auth.service';
import {RegisterEffect} from './store/effects/register.effect';

const routes: Routes = [{path: 'register', component: RegisterComponent}];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    StoreModule.forFeature('auth', reducers),
    EffectsModule.forFeature([RegisterEffect]),
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    FontAwesomeModule,
  ],
  declarations: [RegisterComponent],
  providers: [AuthService],
})
export class AuthModule {}
