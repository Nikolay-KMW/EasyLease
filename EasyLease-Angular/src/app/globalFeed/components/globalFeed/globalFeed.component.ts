import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {setDescriptionAction, setTitleAction} from 'src/app/shared/modules/banner/store/action/sync.action';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';

@Component({
  selector: 'el-global-feed',
  templateUrl: './globalFeed.component.html',
  styleUrls: ['./globalFeed.component.scss'],
})
export class GlobalFeedComponent implements OnInit {
  apiUrl = '/realty';

  constructor(private store: Store<AppStateInterface>) {}

  ngOnInit(): void {
    this.setValueBannerModule();
  }

  setValueBannerModule(): void {
    this.store.dispatch(setTitleAction({title: 'Список объявлений'}));
    this.store.dispatch(setDescriptionAction({description: 'Вы можете разместить здесь свое объявление бесплатно!'}));
  }
}
