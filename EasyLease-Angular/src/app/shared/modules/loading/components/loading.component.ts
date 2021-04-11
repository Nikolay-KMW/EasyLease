import {Component, Input, OnInit} from '@angular/core';
import {LoadingMode} from '../types/loadingMode.type';

@Component({
  selector: 'el-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss'],
})
export class LoadingComponent implements OnInit {
  @Input('nameModule') nameModuleProps: string = '';
  @Input('loadingMode') loadingModeProps: LoadingMode = 'bar';

  constructor() {}

  ngOnInit(): void {}
}
