import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FooterComponent} from './components/footer/footer.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {MatButtonModule} from '@angular/material/button';
import {RouterModule} from '@angular/router';
import {MatDividerModule} from '@angular/material/divider';

@NgModule({
  declarations: [FooterComponent],
  imports: [CommonModule, RouterModule, FontAwesomeModule, MatButtonModule, MatDividerModule],
  exports: [FooterComponent],
  providers: [],
})
export class FooterModule {}
