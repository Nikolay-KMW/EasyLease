import {FocusMonitor, FocusOrigin} from '@angular/cdk/a11y';
import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  ElementRef,
  Input,
  NgZone,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import {MatMenuTrigger} from '@angular/material/menu';
import {faAsymmetrik} from '@fortawesome/free-brands-svg-icons';
import {
  faBars,
  faEllipsisV,
  faPencilAlt,
  faSignInAlt,
  faSmile,
  faUserCog,
  faUserPlus,
  IconDefinition,
} from '@fortawesome/free-solid-svg-icons';
import {select, Store} from '@ngrx/store';
import {Observable, Subscription} from 'rxjs';

import {currentUserSelected, isAnonymousSelected, isLoggedInSelected} from 'src/app/auth/store/selectors';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {CurrentUserInterface} from 'src/app/shared/types/currentUser.interface';
import {closeSidenavAction, openSidenavAction} from '../../store/actions/toggle.action';
import {isOpenedSidenavSelector} from '../../store/selectors';

@Component({
  selector: 'el-top-bar',
  templateUrl: './topBar.component.html',
  styleUrls: ['./topBar.component.scss'],
})
export class TopBarComponent implements OnInit, OnDestroy, AfterViewInit {
  @Input('brandName') brandNameProps: string = 'Brand';

  @ViewChild('menu') element?: ElementRef<HTMLElement>;
  @ViewChild(MatMenuTrigger) trigger?: MatMenuTrigger;

  isLoggedIn$: Observable<boolean | null>;
  isAnonymous$: Observable<boolean>;
  currentUser$: Observable<CurrentUserInterface | null>;
  isOpenedSidenav$: Observable<boolean>;

  subscribeToIsOpenedSidenav$: Subscription;

  isOpened: boolean = false;
  isOpenedMenu: boolean = false;

  faUserPlus: IconDefinition = faUserPlus;
  faUserCog: IconDefinition = faUserCog;
  faSmile: IconDefinition = faSmile;
  faBars: IconDefinition = faBars;
  faAsymmetrik: IconDefinition = faAsymmetrik;
  faPencilAlt: IconDefinition = faPencilAlt;
  faSignInAlt: IconDefinition = faSignInAlt;
  faEllipsisV: IconDefinition = faEllipsisV;

  constructor(
    private store: Store<AppStateInterface>,
    private focusMonitor: FocusMonitor,
    private ngZone: NgZone,
    private cdr: ChangeDetectorRef
  ) {
    this.isLoggedIn$ = this.store.pipe(select(isLoggedInSelected));
    this.isAnonymous$ = this.store.pipe(select(isAnonymousSelected));
    this.currentUser$ = this.store.pipe(select(currentUserSelected));
    this.isOpenedSidenav$ = this.store.pipe(select(isOpenedSidenavSelector));

    this.subscribeToIsOpenedSidenav$ = this.isOpenedSidenav$.subscribe((isOpen) => (this.isOpened = isOpen));
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    // const topBar = this.element.nativeElement;
    // console.log(topBar);
    // this.focusMonitor.stopMonitoring(topBar.getElementById('menuTrigger'));
    // if (this.element) {
    //   this.focusMonitor.stopMonitoring(this.element);
    // }
  }

  openMenu(): void {
    if (this.isOpenedMenu) {
      //this.trigger?.toggleMenu();
      this.isOpenedMenu = false;
    } else {
      //this.trigger?.focus();
      this.isOpenedMenu = true;
    }
    //this.trigger?.focus();
  }
  closeMenu(): void {
    this.trigger?.toggleMenu();
  }

  toggleSidenav(): void {
    if (this.isOpened) {
      this.store.dispatch(closeSidenavAction());
    } else {
      this.store.dispatch(openSidenavAction());
    }
  }

  ngOnDestroy(): void {
    this.subscribeToIsOpenedSidenav$.unsubscribe();
  }
}
