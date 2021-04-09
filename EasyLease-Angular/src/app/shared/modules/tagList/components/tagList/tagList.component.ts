import {Component, Input, OnInit} from '@angular/core';
import {TagType} from 'src/app/shared/types/Tag.type';

@Component({
  selector: 'el-tag-list',
  templateUrl: './tagList.component.html',
  styleUrls: ['./tagList.component.scss'],
})
export class TagListComponent implements OnInit {
  @Input('tags') tagsProps?: TagType[];

  constructor() {}

  ngOnInit(): void {}
}
