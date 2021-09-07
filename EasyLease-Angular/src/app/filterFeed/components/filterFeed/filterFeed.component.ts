import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import {Store} from '@ngrx/store';
import {Subscription} from 'rxjs';

import {setSelectedTagAction} from 'src/app/shared/modules/tags/store/actions/setSelectedTag.action';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {TagType} from 'src/app/shared/types/tag.type';

@Component({
  selector: 'el-filter-feed',
  templateUrl: './filterFeed.component.html',
  styleUrls: ['./filterFeed.component.scss'],
})
export class FilterFeedComponent implements OnInit, OnDestroy {
  apiUrl: string = '/';
  tagName: TagType | null = null;

  routeParamsSubscription: Subscription;

  constructor(private store: Store<AppStateInterface>, private route: ActivatedRoute) {
    this.routeParamsSubscription = this.route.params.subscribe((params: Params) => {
      this.tagName = params.slug;

      if (this.tagName) {
        this.store.dispatch(setSelectedTagAction({selectedTag: this.tagName}));
      }

      this.apiUrl = `/articles?tag=${this.tagName}`;
    });
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.routeParamsSubscription.unsubscribe();
  }
}
