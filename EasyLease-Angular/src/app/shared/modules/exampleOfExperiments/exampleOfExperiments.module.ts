import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatRippleModule} from '@angular/material/core';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {MatButtonModule} from '@angular/material/button';

import {ExampleOfExperimentComponent} from './components/exampleOfExperiment/exampleOfExperiment.component';

@NgModule({
  declarations: [ExampleOfExperimentComponent],
  imports: [CommonModule, MatRippleModule, FontAwesomeModule, MatButtonModule],
  exports: [ExampleOfExperimentComponent],
  providers: [],
})
export class ExampleOfExperimentsModule {}
