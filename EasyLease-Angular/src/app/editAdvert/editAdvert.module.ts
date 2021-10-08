import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {EffectsModule} from '@ngrx/effects';
import {StoreModule} from '@ngrx/store';

import {EditAdvertComponent} from './components/editAdvert/editAdvert.component';
import {AdvertFormModule} from '../shared/modules/advertForm/advertForm.module';
import {EditAdvertService} from './services/editAdvert.service';
import {UpdateAdvertEffect} from './store/effects/updateAdvert.effect';
import {reducers} from './store/reducers';
import {AdvertService as SharedAdvertService} from '../shared/services/advert.service';
import {GetAdvertEffect} from './store/effects/getAdvert.effect';
import {LoadingModule} from '../shared/modules/loading/loading.module';

const routes = [
  {
    path: 'advert/:slug/edit',
    component: EditAdvertComponent,
  },
];

@NgModule({
  declarations: [EditAdvertComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    EffectsModule.forFeature([GetAdvertEffect, UpdateAdvertEffect]),
    StoreModule.forFeature('editAdvert', reducers),
    AdvertFormModule,
    LoadingModule,
  ],
  exports: [],
  providers: [EditAdvertService, SharedAdvertService],
})
export class EditAdvertModule {}
