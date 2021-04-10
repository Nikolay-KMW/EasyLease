import {Component, Input, OnInit} from '@angular/core';
import {select, Store} from '@ngrx/store';
import {Observable} from 'rxjs';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {TagType} from 'src/app/shared/types/Tag.type';
import {getTagsAction} from '../../store/actions/getTags.action';
import {errorSelector, isLoadingSelector, tagsSelector} from '../../store/selectors';

@Component({
  selector: 'el-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.scss'],
})
export class TagsComponent implements OnInit {
  @Input('apiUrl') apiUrlProps?: string;

  isLoading$: Observable<boolean>;
  error$: Observable<string | null>;
  tags$: Observable<TagType[] | null>;

  constructor(private store: Store<AppStateInterface>) {
    this.isLoading$ = this.store.pipe(select(isLoadingSelector));
    this.error$ = this.store.pipe(select(errorSelector));
    this.tags$ = this.store.pipe(select(tagsSelector));
  }

  ngOnInit(): void {
    if (this.apiUrlProps) {
      this.store.dispatch(getTagsAction({url: this.apiUrlProps}));
    }
  }
}
