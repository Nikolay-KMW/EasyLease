import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {BreakpointObserver, Breakpoints, BreakpointState, MediaMatcher} from '@angular/cdk/layout';
import {Observable} from 'rxjs';
import {map, shareReplay} from 'rxjs/operators';

import {select, Store} from '@ngrx/store';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {isOpenedSidenavSelector} from '../../../topBar/store/selectors';
import {closeSidenavAction} from '../../../topBar/store/actions/toggle.action';
import {faEdit, faStar, IconDefinition} from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'el-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.scss'],
})
export class ShellComponent implements OnInit {
  isHandset$: Observable<boolean>;
  isOpenedSidenav$: Observable<boolean>;

  faEdit: IconDefinition = faEdit;
  faStar: IconDefinition = faStar;

  constructor(private breakpointObserver: BreakpointObserver, private store: Store<AppStateInterface>) {
    this.isHandset$ = this.breakpointObserver.observe([Breakpoints.Handset]).pipe(
      map((result: BreakpointState) => result.matches),
      shareReplay()
    );
    this.isOpenedSidenav$ = this.store.pipe(select(isOpenedSidenavSelector));
  }

  ngOnInit(): void {}

  closeSidenav(): void {
    this.store.dispatch(closeSidenavAction());
  }
}
