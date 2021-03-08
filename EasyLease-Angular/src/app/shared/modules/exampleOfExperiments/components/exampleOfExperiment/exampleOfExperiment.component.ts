import {Component, OnInit} from '@angular/core';
import {faMagic, faPencilAlt, faUser, faUserCog, IconDefinition} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'example-experiment',
  templateUrl: './exampleOfExperiment.component.html',
  styleUrls: ['./exampleOfExperiment.component.scss'],
})
export class ExampleOfExperimentComponent implements OnInit {
  faUser: IconDefinition = faUser;
  faUserCog: IconDefinition = faUserCog;
  faPencilAlt: IconDefinition = faPencilAlt;
  faMagic: IconDefinition = faMagic;

  constructor() {}

  ngOnInit(): void {}
}
