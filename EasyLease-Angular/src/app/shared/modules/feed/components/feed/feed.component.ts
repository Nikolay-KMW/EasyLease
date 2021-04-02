import {Component, Input, OnInit} from '@angular/core';
import {select, Store} from '@ngrx/store';
import {Observable} from 'rxjs';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {getFeedAction} from '../../store/actions/getFeed.action';
import {errorSelector, feedSelector, isLoadingSelector} from '../../store/selectors';
import {GetFeedResponseInterface} from '../../types/getFeedResponse.interface';

@Component({
  selector: 'el-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.scss'],
})
export class FeedComponent implements OnInit {
  @Input('apiUrl') apiUrlProps?: string;

  isLoading$: Observable<boolean>;
  error$: Observable<string | null>;
  feed$: Observable<GetFeedResponseInterface | null>;

  constructor(private store: Store<AppStateInterface>) {
    this.isLoading$ = this.store.pipe(select(isLoadingSelector));
    this.error$ = this.store.pipe(select(errorSelector));
    this.feed$ = this.store.pipe(select(feedSelector));
  }

  ngOnInit(): void {
    if (this.apiUrlProps) {
      this.store.dispatch(getFeedAction({url: this.apiUrlProps}));
    }
  }
}
