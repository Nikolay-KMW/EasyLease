import {Component, OnInit} from '@angular/core';
import {IconDefinition} from '@fortawesome/fontawesome-svg-core';
import {faEnvelope} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'el-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent implements OnInit {
  faEnvelope: IconDefinition = faEnvelope;

  constructor() {}

  ngOnInit(): void {}
}
