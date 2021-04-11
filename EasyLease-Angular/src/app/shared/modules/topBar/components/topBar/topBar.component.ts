import {Component, Input, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {MatMenuTrigger} from '@angular/material/menu';
import {faAsymmetrik} from '@fortawesome/free-brands-svg-icons';
import {
  faBars,
  faEllipsisV,
  faList,
  faPencilAlt,
  faSignInAlt,
  faSignOutAlt,
  faSmile,
  faUserCog,
  faUserPlus,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';
import {select, Store} from '@ngrx/store';
import {Observable, Subscription} from 'rxjs';

import {currentUserSelector, isAnonymousSelector, isLoggedInSelector} from 'src/app/auth/store/selectors';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {CurrentUserInterface} from 'src/app/shared/types/currentUser.interface';
import {closeSidenavAction, openSidenavAction} from '../../store/actions/toggle.action';
import {isOpenedSidenavSelector} from '../../store/selectors';

@Component({
  selector: 'el-top-bar',
  templateUrl: './topBar.component.html',
  styleUrls: ['./topBar.component.scss'],
})
export class TopBarComponent implements OnInit, OnDestroy {
  @Input('brandName') brandNameProps: string = 'Brand';

  @ViewChild(MatMenuTrigger) MatMenuTrigger?: MatMenuTrigger;

  isLoggedIn$: Observable<boolean | null>;
  isAnonymous$: Observable<boolean>;
  currentUser$: Observable<CurrentUserInterface | null>;
  isOpenedSidenav$: Observable<boolean>;

  subscribeToIsOpenedSidenav$: Subscription;

  isOpenedNav: boolean = false;
  isOpenedMenu: boolean = false;

  faUserPlus: IconDefinition = faUserPlus;
  faUserCog: IconDefinition = faUserCog;
  faSmile: IconDefinition = faSmile;
  faBars: IconDefinition = faBars;
  faAsymmetrik: IconDefinition = faAsymmetrik;
  faPencilAlt: IconDefinition = faPencilAlt;
  faSignInAlt: IconDefinition = faSignInAlt;
  faEllipsisV: IconDefinition = faEllipsisV;
  faSignOutAlt: IconDefinition = faSignOutAlt;
  faList: IconDefinition = faList;

  constructor(private store: Store<AppStateInterface>) {
    this.isLoggedIn$ = this.store.pipe(select(isLoggedInSelector));
    this.isAnonymous$ = this.store.pipe(select(isAnonymousSelector));
    this.currentUser$ = this.store.pipe(select(currentUserSelector));
    this.isOpenedSidenav$ = this.store.pipe(select(isOpenedSidenavSelector));

    this.subscribeToIsOpenedSidenav$ = this.isOpenedSidenav$.subscribe((isOpen) => (this.isOpenedNav = isOpen));
  }

  ngOnInit(): void {}

  toggleMenu(): void {
    if (this.MatMenuTrigger?.menuOpen) {
      this.MatMenuTrigger?.openMenu();
      this.isOpenedMenu = true;
    } else {
      this.MatMenuTrigger?.closeMenu();
      this.isOpenedMenu = false;
    }
  }
  menuClosed(): void {
    this.MatMenuTrigger?.closeMenu();
    this.isOpenedMenu = false;
  }

  toggleSidenav(): void {
    if (this.isOpenedNav) {
      this.store.dispatch(closeSidenavAction());
    } else {
      this.store.dispatch(openSidenavAction());
    }
  }

  ngOnDestroy(): void {
    this.subscribeToIsOpenedSidenav$.unsubscribe();
  }
}
