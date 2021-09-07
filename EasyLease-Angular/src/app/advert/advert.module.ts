import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {EffectsModule} from '@ngrx/effects';
import {StoreModule} from '@ngrx/store';
import {RouterModule} from '@angular/router';
import {MatDividerModule} from '@angular/material/divider';
import {HammerModule} from '@angular/platform-browser';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';

import {GetAdvertEffect} from './store/effects/getAdvert.effect';
import {reducers} from './store/reducers';
import {MatButtonModule} from '@angular/material/button';
import {AdvertService as ShardAdvertService} from '../shared/services/advert.service';
import {LoadingModule} from '../shared/modules/loading/loading.module';
import {ErrorMessageModule} from '../shared/modules/errorMessage/errorMessage.module';
import {AdvertComponent} from './components/advert/advert.component';
import {MatCarouselModule} from 'ng-mat-carousel';
import {TagListModule} from '../shared/modules/tagList/tagList.module';
import {AdvertService} from './services/advert.service';
import {DeleteAdvertEffect} from './store/effects/deleteAdvert.effect';
import {ComfortListModule} from '../shared/modules/comfortList/comfortList.module';
import {AddToFavoritesModule} from '../shared/modules/addToFavorites/addToFavorites.module';

const routes = [{path: 'advert/:slug', component: AdvertComponent}];

@NgModule({
  declarations: [AdvertComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatDividerModule,
    MatButtonModule,
    ErrorMessageModule,
    LoadingModule,
    ComfortListModule,
    TagListModule,
    AddToFavoritesModule,
    FontAwesomeModule,
    HammerModule,
    MatCarouselModule.forRoot(),
    EffectsModule.forFeature([GetAdvertEffect, DeleteAdvertEffect]),
    StoreModule.forFeature('advert', reducers),
  ],
  exports: [],
  providers: [ShardAdvertService, AdvertService],
})
export class AdvertModule {}
