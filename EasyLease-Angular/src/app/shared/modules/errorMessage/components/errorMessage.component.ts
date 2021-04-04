import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'el-error-message',
  templateUrl: './errorMessage.component.html',
  styleUrls: ['./errorMessage.component.scss'],
})
export class ErrorMessageComponent implements OnInit {
  @Input('message') messageProps: string = 'Что-то пошло не так...';

  constructor() {}

  ngOnInit(): void {}
}
