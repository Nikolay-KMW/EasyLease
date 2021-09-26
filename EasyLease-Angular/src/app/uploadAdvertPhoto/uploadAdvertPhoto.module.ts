import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {StoreModule} from '@ngrx/store';
import {EffectsModule} from '@ngrx/effects';

import {UploadAdvertPhotoComponent} from './components/uploadAdvertPhoto/uploadAdvertPhoto.component';
import {UploadPhotoService} from './services/uploadPhoto.service';
import {UploadPhotoEffect} from './store/effects/uploadPhoto.effect';
import {reducers} from './store/reducers';
import {PhotoDropzoneModule} from '../shared/modules/photoDropzone/photoDropzone.module';
import {LoadingModule} from '../shared/modules/loading/loading.module';
import {ErrorMessageModule} from '../shared/modules/errorMessage/errorMessage.module';

const routes = [{path: 'adverts/:slug/photos', component: UploadAdvertPhotoComponent}];

@NgModule({
  declarations: [UploadAdvertPhotoComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    EffectsModule.forFeature([UploadPhotoEffect]),
    StoreModule.forFeature('uploadAdvertPhoto', reducers),
    PhotoDropzoneModule,
    LoadingModule,
    ErrorMessageModule,
  ],
  exports: [],
  providers: [UploadPhotoService],
})
export class UploadAdvertPhotoModule {}
