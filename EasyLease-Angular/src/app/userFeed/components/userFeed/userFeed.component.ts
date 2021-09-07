import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import {Store} from '@ngrx/store';
import {Subscription} from 'rxjs';
import {setDescriptionAction, setTitleAction} from 'src/app/shared/modules/banner/store/action/sync.action';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';

@Component({
  selector: 'el-user-feed',
  templateUrl: './userFeed.component.html',
  styleUrls: ['./userFeed.component.scss'],
})
export class UserFeedComponent implements OnInit, OnDestroy {
  apiUrl: string = '/';
  // userId: string | null = null;

  routeParamsSubscription: Subscription;

  constructor(private store: Store<AppStateInterface>, private route: ActivatedRoute) {
    this.routeParamsSubscription = this.route.params.subscribe((params: Params) => {
      let userId = params.slug as string;
      this.apiUrl = `/profile/${userId}/adverts`;
    });
  }

  ngOnInit(): void {
    this.setValueBannerModule();
  }

  setValueBannerModule(): void {
    this.store.dispatch(setTitleAction({title: 'Список Ваших объявлений'}));
    this.store.dispatch(setDescriptionAction({description: 'Вы можете разместить здесь свое объявление бесплатно!'}));
  }

  ngOnDestroy(): void {
    this.routeParamsSubscription.unsubscribe();
  }
}
