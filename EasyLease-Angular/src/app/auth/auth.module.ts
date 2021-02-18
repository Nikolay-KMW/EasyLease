import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MatInputModule} from '@angular/material/input';
import {ReactiveFormsModule} from '@angular/forms';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

//import {RegisterComponent} from './components/register/register.component';
import {RegisterComponent} from 'src/app/auth/components/register/register.component';

const routes: Routes = [{path: 'register', component: RegisterComponent}];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes), ReactiveFormsModule, MatInputModule, BrowserAnimationsModule],
  declarations: [RegisterComponent],
})
export class AuthModule {}
