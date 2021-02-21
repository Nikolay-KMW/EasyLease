import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MatInputModule} from '@angular/material/input';
import {ReactiveFormsModule} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';

//import {RegisterComponent} from './components/register/register.component';
import {RegisterComponent} from 'src/app/auth/components/register/register.component';

const routes: Routes = [{path: 'register', component: RegisterComponent}];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
  ],
  declarations: [RegisterComponent],
})
export class AuthModule {}
