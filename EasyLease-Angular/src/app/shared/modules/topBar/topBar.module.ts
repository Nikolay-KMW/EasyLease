import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatToolbarModule} from '@angular/material/toolbar';
import {TopBarComponent} from './components/topBar/topBar.component';
import {RouterModule} from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import {MatRippleModule} from '@angular/material/core';

@NgModule({
  declarations: [TopBarComponent],
  imports: [CommonModule, MatToolbarModule, RouterModule, MatIconModule, MatRippleModule],
  exports: [TopBarComponent],
  providers: [],
})
export class TopBarModule {}
