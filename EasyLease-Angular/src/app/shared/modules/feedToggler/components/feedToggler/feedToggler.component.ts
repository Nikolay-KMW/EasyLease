import {Component, OnDestroy, OnInit} from '@angular/core';
import {NavigationEnd, Router} from '@angular/router';
import {select, Store} from '@ngrx/store';
import {Subscription} from 'rxjs';

import {currentUserSelector, isLoggedInSelector} from 'src/app/auth/store/selectors';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {TabLink} from 'src/app/shared/modules/feedToggler/types/tabLink.type';
import {TagType} from 'src/app/shared/types/Tag.type';
import {selectedTagSelector} from '../../../tags/store/selectors';
import {parseUrl} from 'query-string';

@Component({
  selector: 'el-feed-toggler',
  templateUrl: './feedToggler.component.html',
  styleUrls: ['./feedToggler.component.scss'],
})
export class FeedTogglerComponent implements OnInit, OnDestroy {
  currentUserSubscription: Subscription;
  tagNameSubscription: Subscription;
  isLoggedInSubscription: Subscription;
  navigationEndSubscription: Subscription;

  tagName: TagType | null = null;
  tabs!: TabLink[];

  userName: string | null = null;
  isLoggedIn: boolean = false;
  activeLink: string = '/';
  isVisible: boolean = false;

  constructor(private store: Store<AppStateInterface>, private router: Router) {
    // this.tabs = [new TabLink('Все объявления', '/', true)];
    // this.activeLink = this.tabs[0].link;

    this.currentUserSubscription = store.pipe(select(currentUserSelector)).subscribe((currentUser) => {
      if (currentUser) {
        this.userName = currentUser.username;
        this.ngOnInit();
      }
    });

    this.tagNameSubscription = store.pipe(select(selectedTagSelector)).subscribe((selectedTag) => {
      this.tagName = selectedTag;
      this.ngOnInit();
    });

    this.isLoggedInSubscription = this.store.pipe(select(isLoggedInSelector)).subscribe((isLoggedIn) => {
      isLoggedIn == null ? (this.isLoggedIn = false) : (this.isLoggedIn = isLoggedIn);
      this.ngOnInit();
    });

    this.navigationEndSubscription = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.ngOnInit();
      }
    });
  }

  ngOnInit(): void {
    this.tabs = [
      new TabLink('Мои объявления', `/profile/${this.userName}`, this.isLoggedIn),
      new TabLink('Избранные', `/profile/${this.userName}/favorites`, this.isLoggedIn),
      new TabLink('Все объявления', '/', true),
      new TabLink(`Фильтр по тегу: #${this.tagName}`, `/tags/${this.tagName}`, this.tagName == null ? false : true),
    ];

    this.isVisible = false;

    const parsedUrl = parseUrl(this.router.url);

    for (const tab of this.tabs) {
      if (parsedUrl.url === tab.link) {
        this.isVisible = true;
        this.activeLink = tab.link;
      }
    }
  }

  ngOnDestroy(): void {
    this.currentUserSubscription.unsubscribe();
    this.navigationEndSubscription.unsubscribe();
    this.isLoggedInSubscription.unsubscribe();
    this.tagNameSubscription.unsubscribe();
  }
}
