import {Component, OnInit} from '@angular/core';
import {faCog, faLongArrowAltUp} from '@fortawesome/free-solid-svg-icons';
import {faTwitter, faFacebook} from '@fortawesome/free-brands-svg-icons';
import {Store} from '@ngrx/store';
import {AppStateInterface} from './shared/types/appState.interface';
import {getCurrentUserAction} from './auth/store/actions/getCurrentUser.action';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  brandName = 'Easy Lease';
  // faCog = faCog;
  // faLongArrowAltUp = faLongArrowAltUp;
  // faTwitter = faTwitter;
  // faFacebook = faFacebook;

  constructor(private store: Store<AppStateInterface>) {}

  ngOnInit(): void {
    this.store.dispatch(getCurrentUserAction());
  }
}
