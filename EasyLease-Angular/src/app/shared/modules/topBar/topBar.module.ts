import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatToolbarModule} from '@angular/material/toolbar';
import {TopBarComponent} from './components/topBar/topBar.component';
import {RouterModule} from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import {MatRippleModule} from '@angular/material/core';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [TopBarComponent],
  imports: [CommonModule, MatToolbarModule, RouterModule, MatRippleModule, FontAwesomeModule],
  exports: [TopBarComponent],
  providers: [],
})
export class TopBarModule {}
