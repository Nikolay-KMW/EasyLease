import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';

import {AuthModule} from 'src/app/auth/auth.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    // FontAwesomeModule,
    AuthModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
