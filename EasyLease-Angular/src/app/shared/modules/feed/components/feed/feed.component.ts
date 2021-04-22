import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges} from '@angular/core';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {select, Store} from '@ngrx/store';
import {parseUrl, stringify} from 'query-string';
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
export class FeedComponent implements OnInit, OnChanges, OnDestroy {
  @Input('apiUrl') apiUrlProps?: string;

  isLoading$: Observable<boolean>;
  error$: Observable<string | null>;
  feed$: Observable<GetFeedResponseInterface | null>;

  limit: number;
  baseUrl!: string;
  queryParamsSubscription: Subscription;
  currentPage: number = 1;

  constructor(private store: Store<AppStateInterface>, private router: Router, private route: ActivatedRoute) {
    this.isLoading$ = this.store.pipe(select(isLoadingSelector));
    this.error$ = this.store.pipe(select(errorSelector));
    this.feed$ = this.store.pipe(select(feedSelector));

    this.limit = environment.limit;
    //this.baseUrl = router.url.split('?')[0];
    this.queryParamsSubscription = this.route.queryParams.subscribe((params: Params) => {
      this.currentPage = Number(params.page || '1');
      this.ngOnInit();
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    const isApiUrlChanged =
      !changes.apiUrlProps.firstChange && changes.apiUrlProps.currentValue !== changes.apiUrlProps.previousValue;

    if (isApiUrlChanged) {
      this.ngOnInit();
    }
  }

  ngOnInit(): void {
    this.baseUrl = this.router.url.split('?')[0];

    const offset = this.currentPage * this.limit - this.limit;

    if (this.apiUrlProps) {
      const parsedUrl = parseUrl(this.apiUrlProps);
      const stringifiedParams = stringify({limit: this.limit, offset, ...parsedUrl.query});
      const apiUrlWithParams = `${parsedUrl.url}?${stringifiedParams}`;

      this.store.dispatch(getFeedAction({url: apiUrlWithParams}));
    }
  }

  ngOnDestroy(): void {
    this.queryParamsSubscription.unsubscribe();
  }
}
