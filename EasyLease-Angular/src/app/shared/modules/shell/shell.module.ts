import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';

import {ShellComponent} from './components/shell/shell.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import {RouterModule} from '@angular/router';

@NgModule({
  declarations: [ShellComponent],
  imports: [
    CommonModule,
    RouterModule,
    MatSidenavModule,
    MatListModule,
    FontAwesomeModule,
    MatDividerModule,
    MatButtonModule,
  ],
  exports: [ShellComponent],
  providers: [],
})
export class ShellModule {}
