import {Component, Input, OnInit} from '@angular/core';
import {faAsymmetrik} from '@fortawesome/free-brands-svg-icons';
import {
  faBars,
  faPencilAlt,
  faSignInAlt,
  faSmile,
  faUser,
  faUserCog,
  faUserPlus,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';
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

  faUser: IconDefinition = faUser;
  faUserPlus: IconDefinition = faUserPlus;
  faUserCog: IconDefinition = faUserCog;
  faSmile: IconDefinition = faSmile;
  faBars: IconDefinition = faBars;
  faAsymmetrik: IconDefinition = faAsymmetrik;
  faPencilAlt: IconDefinition = faPencilAlt;
  faSignInAlt: IconDefinition = faSignInAlt;

  constructor(private store: Store<AppStateInterface>) {
    this.isLoggedIn$ = this.store.pipe(select(isLoggedInSelected));
    this.isAnonymous$ = this.store.pipe(select(isAnonymousSelected));
    this.currentUser$ = this.store.pipe(select(currentUserSelected));
  }

  ngOnInit(): void {}
}
