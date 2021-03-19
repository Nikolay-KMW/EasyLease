import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatToolbarModule} from '@angular/material/toolbar';
import {TopBarComponent} from './components/topBar/topBar.component';
import {RouterModule} from '@angular/router';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {MatButtonModule} from '@angular/material/button';

@NgModule({
  declarations: [TopBarComponent],
  imports: [CommonModule, MatToolbarModule, RouterModule, FontAwesomeModule, MatButtonModule],
  exports: [TopBarComponent],
  providers: [],
})
export class TopBarModule {}
