import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CreateAdvertComponent} from './components/createAdvert/createAdvert.component';
import {RouterModule} from '@angular/router';

import {AdvertFormModule} from '../shared/modules/advertForm/advertForm.module';
import {CreateAdvertService} from './services/createAdvert.service';
import {EffectsModule} from '@ngrx/effects';
import {CreateAdvertEffect} from './store/effects/createAdvert.effect';
import {StoreModule} from '@ngrx/store';
import {reducers} from './store/reducers';

const routes = [
  {
    path: 'advert/new',
    component: CreateAdvertComponent,
  },
];

@NgModule({
  declarations: [CreateAdvertComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    EffectsModule.forFeature([CreateAdvertEffect]),
    StoreModule.forFeature('createAdvert', reducers),
    AdvertFormModule,
  ],
  exports: [],
  providers: [CreateAdvertService],
})
export class CreateAdvertModule {}
