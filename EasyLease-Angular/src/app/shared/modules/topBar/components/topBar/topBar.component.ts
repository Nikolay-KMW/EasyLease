import {Component, Input, OnInit} from '@angular/core';
import {select, Store} from '@ngrx/store';
import {Observable} from 'rxjs';

import {currentUserSelected, isAnonymousSelected, isLoggedInSelected} from 'src/app/auth/store/selectors';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {CurrentUserInterface} from 'src/app/shared/types/currentUser.interface';

@Component({
  selector: 'el-top-bar',
  templateUrl: './topBar.component.html',
  styleUrls: ['./topBar.component.scss'],
})
export class TopBarComponent implements OnInit {
  @Input('brandName') brandNameProps: string = 'Brand';

  isLoggedIn$: Observable<boolean | null>;
  isAnonymous$: Observable<boolean>;
  currentUser$: Observable<CurrentUserInterface | null>;

  constructor(private store: Store<AppStateInterface>) {
    this.isLoggedIn$ = this.store.pipe(select(isLoggedInSelected));
    this.isAnonymous$ = this.store.pipe(select(isAnonymousSelected));
    this.currentUser$ = this.store.pipe(select(currentUserSelected));
  }

  ngOnInit(): void {}
}
