import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatToolbarModule} from '@angular/material/toolbar';
import {TopBarComponent} from './components/topBar/topBar.component';
import {RouterModule} from '@angular/router';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {MatButtonModule} from '@angular/material/button';
import {MatMenuModule} from '@angular/material/menu';
import {StoreModule} from '@ngrx/store';

import {reducers} from './store/reducers';
import {MatDividerModule} from '@angular/material/divider';

@NgModule({
  declarations: [TopBarComponent],
  imports: [
    CommonModule,
    MatToolbarModule,
    RouterModule,
    FontAwesomeModule,
    MatButtonModule,
    MatMenuModule,
    MatDividerModule,
    StoreModule.forFeature('topBar', reducers),
  ],
  exports: [TopBarComponent],
  providers: [],
})
export class TopBarModule {}
