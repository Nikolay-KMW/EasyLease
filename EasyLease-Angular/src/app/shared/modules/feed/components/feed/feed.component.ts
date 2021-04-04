import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {select, Store} from '@ngrx/store';
import {Observable, Subscription} from 'rxjs';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {environment} from 'src/environments/environment';
import {getFeedAction} from '../../store/actions/getFeed.action';
import {errorSelector, feedSelector, isLoadingSelector} from '../../store/selectors';
import {GetFeedResponseInterface} from '../../types/getFeedResponse.interface';

@Component({
  selector: 'el-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.scss'],
})
export class FeedComponent implements OnInit, OnDestroy {
  @Input('apiUrl') apiUrlProps?: string;

  isLoading$: Observable<boolean>;
  error$: Observable<string | null>;
  feed$: Observable<GetFeedResponseInterface | null>;

  limit: number;
  baseUrl: string;
  queryParamsSubscription: Subscription;
  currentPage: number = 1;

  constructor(private store: Store<AppStateInterface>, private router: Router, private route: ActivatedRoute) {
    this.isLoading$ = this.store.pipe(select(isLoadingSelector));
    this.error$ = this.store.pipe(select(errorSelector));
    this.feed$ = this.store.pipe(select(feedSelector));

    this.limit = environment.limit;
    this.baseUrl = router.url.split('?')[0];
    this.queryParamsSubscription = this.route.queryParams.subscribe((params: Params) => {
      this.currentPage = Number(params.page || '1');
    });
  }

  ngOnInit(): void {
    if (this.apiUrlProps) {
      this.store.dispatch(getFeedAction({url: this.apiUrlProps}));
    }
  }

  ngOnDestroy(): void {
    this.queryParamsSubscription.unsubscribe();
  }
}
