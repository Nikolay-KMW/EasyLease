import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import {Store} from '@ngrx/store';
import {Subscription} from 'rxjs';
import {setDescriptionAction, setTitleAction} from 'src/app/shared/modules/banner/store/action/sync.action';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';

@Component({
  selector: 'el-favorited-feed',
  templateUrl: './favoritedFeed.component.html',
  styleUrls: ['./favoritedFeed.component.scss'],
})
export class FavoritedFeedComponent implements OnInit, OnDestroy {
  //apiUrl: string = '/';
  apiUrl: string = '/profile/favorite-realty';

  //routeParamsSubscription: Subscription;

  constructor(private store: Store<AppStateInterface>, private route: ActivatedRoute) {
    // this.routeParamsSubscription = this.route.params.subscribe((params: Params) => {
    //   this.apiUrl = '/profile/favorite-adverts';
    // });
  }

  ngOnInit(): void {
    this.setValueBannerModule();
  }

  setValueBannerModule(): void {
    this.store.dispatch(setTitleAction({title: 'Ваш список избранных объявлений'}));
    this.store.dispatch(
      setDescriptionAction({
        description: 'Вы можете убирать или добавлять нужные Вам объявления при нажатии на сердечко',
      })
    );
  }

  ngOnDestroy(): void {
    //this.routeParamsSubscription.unsubscribe();
  }
}
