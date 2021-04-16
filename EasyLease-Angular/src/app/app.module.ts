import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
//import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {StoreModule} from '@ngrx/store';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {EffectsModule} from '@ngrx/effects';
import {StoreDevtoolsModule} from '@ngrx/store-devtools';

import {AuthModule} from 'src/app/auth/auth.module';
import {environment} from 'src/environments/environment';
import {TopBarModule} from './shared/modules/topBar/topBar.module';
import {ExampleOfExperimentsModule} from './shared/modules/exampleOfExperiments/exampleOfExperiments.module';
import {PersistanceService} from './shared/services/persistance.service';
import {AuthInterceptor} from './shared/services/authInterceptor.service';
import {GlobalFeedModule} from './globalFeed/globalFeed.module';
import {ShellModule} from './shared/modules/shell/shell.module';
import {routerReducer, StoreRouterConnectingModule} from '@ngrx/router-store';
import {YourFeedModule} from './yourFeed/yourFeed.module';
import {FeedTogglerModule} from './shared/modules/feedToggler/feedToggler.module';
import {BannerModule} from './shared/modules/banner/banner.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    StoreModule.forRoot({router: routerReducer}),
    StoreDevtoolsModule.instrument({maxAge: 25, logOnly: environment.production}),
    EffectsModule.forRoot([]),
    StoreRouterConnectingModule.forRoot(),
    // FontAwesomeModule,
    AuthModule,
    TopBarModule,
    ShellModule,
    GlobalFeedModule,
    YourFeedModule,
    BannerModule,
    FeedTogglerModule,
    ExampleOfExperimentsModule,
    HttpClientModule,
  ],
  providers: [
    PersistanceService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
