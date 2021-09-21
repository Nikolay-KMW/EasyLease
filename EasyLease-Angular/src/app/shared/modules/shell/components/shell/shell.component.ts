import {Component, OnInit} from '@angular/core';
import {BreakpointObserver, Breakpoints, BreakpointState, MediaMatcher} from '@angular/cdk/layout';
import {Observable, Subscription} from 'rxjs';
import {map, shareReplay} from 'rxjs/operators';

import {select, Store} from '@ngrx/store';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {isOpenedSidenavSelector} from '../../../topBar/store/selectors';
import {closeSidenavAction} from '../../../topBar/store/actions/toggle.action';
import {faEdit, faStar, IconDefinition} from '@fortawesome/free-regular-svg-icons';
import {UtilsService} from 'src/app/shared/services/utils.service';
import {isLoggedInSelector} from 'src/app/auth/store/selectors';
import {Router} from '@angular/router';

@Component({
  selector: 'el-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.scss'],
})
export class ShellComponent implements OnInit {
  isHandset$: Observable<boolean>;
  isOpenedSidenav$: Observable<boolean>;
  isLoggedIn: boolean | null = false;

  isLoggedInSubscription: Subscription;

  faEdit: IconDefinition = faEdit;
  faStar: IconDefinition = faStar;

  constructor(
    private breakpointObserver: BreakpointObserver,
    private store: Store<AppStateInterface>,
    private router: Router,
    private utilService: UtilsService
  ) {
    this.isHandset$ = this.breakpointObserver.observe([Breakpoints.Handset, Breakpoints.TabletPortrait]).pipe(
      map((result: BreakpointState) => result.matches),
      shareReplay()
    );
    this.isOpenedSidenav$ = this.store.pipe(select(isOpenedSidenavSelector));
    this.isLoggedInSubscription = this.store
      .pipe(select(isLoggedInSelector))
      .subscribe((isLoggedIn) => (this.isLoggedIn = isLoggedIn));
  }

  ngOnInit(): void {}

  closeSidenav(): void {
    this.store.dispatch(closeSidenavAction());
  }

  goTo(): void {
    if (this.isLoggedIn) {
      this.router.navigateByUrl('/advert');
    } else {
      this.router.navigateByUrl('/authentication/login');
    }

    this.closeSidenav();
  }

  scrollToFooter(className: string): void {
    this.utilService.scrollTo(className, 'smooth', 'center');
    setTimeout(() => this.closeSidenav(), 1000);
  }

  ngOnDestroy(): void {
    this.isLoggedInSubscription.unsubscribe();
  }
}
