import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {NavigationEnd, Router} from '@angular/router';
import {select, Store} from '@ngrx/store';
import {Observable, Subscription} from 'rxjs';
import {isLoggedInSelector} from 'src/app/auth/store/selectors';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';

@Component({
  selector: 'el-feed-toggler',
  templateUrl: './feedToggler.component.html',
  styleUrls: ['./feedToggler.component.scss'],
})
export class FeedTogglerComponent implements OnInit, OnDestroy {
  @Input('tagName') tagNameProps?: string | null;
  //@Input('visibleByUrl') visibleByUrlProps?: string[] | null;

  //visibleByUrlProps: string[] = ['/feed', '/', '/tag'];

  tabItems: TabItem[];

  navigationEndSubscription: Subscription;

  //isLoggedIn$: Observable<boolean | null>;

  isLoggedInSubscription$: Subscription;
  isLoggedIn: boolean = false;
  activeLink: string = '';
  isVisible: boolean = false;

  constructor(private store: Store<AppStateInterface>, private router: Router) {
    this.isLoggedInSubscription$ = this.store
      .pipe(select(isLoggedInSelector))
      .subscribe((isLoggedIn) => (isLoggedIn == null ? (this.isLoggedIn = false) : (this.isLoggedIn = isLoggedIn)));

    // if (this.visibleByUrlProps) {
    //   this.activeLink = this.visibleByUrlProps[0];
    // }

    this.tabItems = [
      new TabItem('Отслеживаемые', '/feed', this.isLoggedIn),
      new TabItem('Все объявления', '/', true),
      new TabItem('Фильтр по тегу:', '/tag', this.tagNameProps == null ? false : true),
    ];

    this.activeLink = this.tabItems[1].nameItem;

    this.navigationEndSubscription = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.ngOnInit();
      }
    });
  }

  ngOnInit(): void {
    this.isVisible = false;

    let navUrl = this.router.url.split('/')[1];
    navUrl = `/${navUrl}`;

    // this.visibleByUrlProps?.forEach((url) => {
    //   if (navUrl === url) {
    //     this.isVisible = true;
    //     this.activeLink = url;
    //   }
    // });

    //console.log(`visible:${this.isVisible}, visibleByUrl:${this.visibleByUrlProps}, url:${this.router.url}`);
  }

  ngOnDestroy(): void {
    this.navigationEndSubscription.unsubscribe();
    this.isLoggedInSubscription$.unsubscribe();
  }
}
