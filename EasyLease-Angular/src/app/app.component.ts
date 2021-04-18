import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {AppStateInterface} from './shared/types/appState.interface';
import {getCurrentUserAction} from './auth/store/actions/getCurrentUser.action';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  brandName = 'Easy Lease';

  constructor(private store: Store<AppStateInterface>) {}

  ngOnInit(): void {
    this.store.dispatch(getCurrentUserAction());
  }
}
