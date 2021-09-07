import {Component, Input, OnInit} from '@angular/core';
import {ComfortType} from 'src/app/shared/types/comfort.type';

@Component({
  selector: 'el-comfort-list',
  templateUrl: './comfortList.component.html',
  styleUrls: ['./comfortList.component.scss'],
})
export class ComfortListComponent implements OnInit {
  @Input('comfortList') ComfortListProps?: ComfortType[];

  constructor() {}

  ngOnInit(): void {}
}
