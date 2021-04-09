import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TagsComponent} from './components/tags/tags.component';
import {TagsService} from './services/tags.service';
import {EffectsModule} from '@ngrx/effects';
import {StoreModule} from '@ngrx/store';
import {reducers} from './store/reducers';
import {GetTagsEffect} from './store/effects/getTags.effect';
import {ErrorMessageModule} from '../errorMessage/errorMessage.module';
import {LoadingModule} from '../loading/loading.module';
import {MatChipsModule} from '@angular/material/chips';
import {RouterModule} from '@angular/router';

@NgModule({
  declarations: [TagsComponent],
  imports: [
    CommonModule,
    RouterModule,
    ErrorMessageModule,
    LoadingModule,
    MatChipsModule,
    EffectsModule.forFeature([GetTagsEffect]),
    StoreModule.forFeature('tags', reducers),
  ],
  exports: [TagsComponent],
  providers: [TagsService],
})
export class TagsModule {}
