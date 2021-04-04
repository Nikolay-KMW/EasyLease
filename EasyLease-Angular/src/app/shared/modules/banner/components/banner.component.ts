import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'el-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss'],
})
export class BannerComponent implements OnInit {
  @Input('title') titleProps: string = '';
  @Input('description') descriptionProps: string = '';

  constructor() {}

  ngOnInit(): void {}
}
