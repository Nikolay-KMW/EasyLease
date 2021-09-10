import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
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
import {PersistenceService} from './shared/services/persistence.service';
import {AuthInterceptor} from './shared/services/authInterceptor.service';
import {GlobalFeedModule} from './globalFeed/globalFeed.module';
import {ShellModule} from './shared/modules/shell/shell.module';
import {routerReducer, StoreRouterConnectingModule} from '@ngrx/router-store';
//import {YourFeedModule} from './yourFeed/yourFeed.module';
import {FeedTogglerModule} from './shared/modules/feedToggler/feedToggler.module';
import {BannerModule} from './shared/modules/banner/banner.module';
import {FilterFeedModule} from './filterFeed/filterFeed.module';
import {AdvertModule} from './advert/advert.module';
import {CreateAdvertModule} from './createAdvert/createAdvert.module';
import {EditAdvertModule} from './editAdvert/editAdvert.module';
import {SettingsModule} from './settings/settings.module';
import {UserFeedModule} from './userFeed/userFeed.module';
import {FavoritedFeedModule} from './favoritedFeed/favoritedFeed.module';
import {UserProfileModule} from './userProfile/userProfile.module';
import {FooterModule} from './shared/modules/footer/footer.module';
import {UtilsService} from './shared/services/utils.service';

@NgModule({
  declarations: [AppComponent],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    StoreModule.forRoot({router: routerReducer}),
    StoreDevtoolsModule.instrument({maxAge: 25, logOnly: environment.production}),
    EffectsModule.forRoot([]),
    StoreRouterConnectingModule.forRoot(),
    AuthModule,
    TopBarModule,
    ShellModule,
    GlobalFeedModule,
    FavoritedFeedModule,
    FilterFeedModule,
    UserFeedModule,
    BannerModule,
    FeedTogglerModule,
    CreateAdvertModule,
    AdvertModule,
    EditAdvertModule,
    SettingsModule,
    UserProfileModule,
    FooterModule,
    //ExampleOfExperimentsModule,
  ],
  providers: [
    PersistenceService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    UtilsService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
