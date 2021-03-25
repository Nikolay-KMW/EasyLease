import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';

import {ShellComponent} from './components/shell/shell.component';

@NgModule({
  declarations: [ShellComponent],
  imports: [CommonModule, MatSidenavModule, MatListModule],
  exports: [ShellComponent],
  providers: [],
})
export class ShellModule {}
