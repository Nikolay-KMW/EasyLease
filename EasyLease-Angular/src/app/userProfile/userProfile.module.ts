import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {UserProfileComponent} from './components/userProfile/userProfile.component';
import {RouterModule} from '@angular/router';
import {UserProfileService} from './services/userProfile';
import {EffectsModule} from '@ngrx/effects';
import {GetUserProfileEffect} from './store/effects/getUserProfile.effect';
import {StoreModule} from '@ngrx/store';
import {reducers} from './store/reducer';
import {MatButtonModule} from '@angular/material/button';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {ErrorMessageModule} from '../shared/modules/errorMessage/errorMessage.module';
import {LoadingModule} from '../shared/modules/loading/loading.module';
import {FeedModule} from '../shared/modules/feed/feed.module';
import {MatTabsModule} from '@angular/material/tabs';
import {MatDividerModule} from '@angular/material/divider';

const routes = [
  {
    path: 'profile/:slug',
    component: UserProfileComponent,
  },
];

@NgModule({
  declarations: [UserProfileComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    EffectsModule.forFeature([GetUserProfileEffect]),
    StoreModule.forFeature('userProfile', reducers),
    MatButtonModule,
    MatTabsModule,
    MatDividerModule,
    ErrorMessageModule,
    LoadingModule,
    FeedModule,
    FontAwesomeModule,
  ],
  exports: [],
  providers: [UserProfileService],
})
export class UserProfileModule {}
