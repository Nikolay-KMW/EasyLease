import {Component, OnInit} from '@angular/core';
import {select, Store} from '@ngrx/store';
import {Observable} from 'rxjs';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {descriptionSelector, titleSelector} from '../../store/selectors';

@Component({
  selector: 'el-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss'],
})
export class BannerComponent implements OnInit {
  title$: Observable<string | null>;
  description$: Observable<string | null>;

  constructor(private store: Store<AppStateInterface>) {
    this.title$ = this.store.pipe(select(titleSelector));
    this.description$ = this.store.pipe(select(descriptionSelector));
  }

  ngOnInit(): void {}
}
