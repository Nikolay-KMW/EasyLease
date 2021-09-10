import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {AppStateInterface} from './shared/types/appState.interface';
import {getCurrentUserAction} from './auth/store/actions/getCurrentUser.action';
import {Subscription} from 'rxjs';
import {NavigationEnd, Router} from '@angular/router';
import {filter} from 'rxjs/operators';
import {UtilsService} from './shared/services/utils.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  brandName = 'Easy Lease';

  routerSubscription: Subscription;

  constructor(private store: Store<AppStateInterface>, private router: Router, private utilService: UtilsService) {
    this.routerSubscription = this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => setTimeout(() => this.utilService.scrollTo('el-banner', 'smooth', 'start'), 250));
  }

  ngOnInit(): void {
    this.store.dispatch(getCurrentUserAction());
  }

  ngOnDestroy() {
    this.routerSubscription?.unsubscribe();
  }
}
