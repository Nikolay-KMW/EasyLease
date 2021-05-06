import {Component, Input, OnInit} from '@angular/core';
import {UtilsService} from 'src/app/shared/services/utils.service';

@Component({
  selector: 'el-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent implements OnInit {
  @Input('total') totalProps?: number;
  @Input('limit') limitProps?: number;
  @Input('currentPage') currentPageProps: number = 1;
  @Input('url') urlProps?: string;

  pagesCount: number = 0;
  pages?: number[];

  constructor(private utilsService: UtilsService) {}

  ngOnInit(): void {
    if (this.totalProps && this.limitProps) {
      this.pagesCount = Math.ceil(this.totalProps / this.limitProps);
      this.pages = this.utilsService.range(1, this.pagesCount);
    }
  }
}
