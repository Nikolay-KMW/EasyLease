import {Component, OnDestroy, OnInit} from '@angular/core';
import {NavigationEnd, Router} from '@angular/router';
import {select, Store} from '@ngrx/store';
import {Subscription} from 'rxjs';

import {isLoggedInSelector} from 'src/app/auth/store/selectors';
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
  tagName: TagType | null = null;
  tabs!: TabLink[];

  tagNameSubscription: Subscription;
  isLoggedInSubscription: Subscription;
  navigationEndSubscription: Subscription;

  isLoggedIn: boolean = false;
  activeLink!: string;
  isVisible: boolean = false;

  constructor(private store: Store<AppStateInterface>, private router: Router) {
    // this.tabs = [new TabLink('Все объявления', '/', true)];
    // this.activeLink = this.tabs[0].link;

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
      new TabLink('Отслеживаемые', '/feed', this.isLoggedIn),
      new TabLink('Все объявления', '/', true),
      new TabLink(`Фильтр по тегу: #${this.tagName}`, '/tags', this.tagName == null ? false : true),
    ];

    this.activeLink = this.tabs[1].link;
    this.isVisible = false;

    const parsedUrl = parseUrl(this.router.url);

    let navUrl = parsedUrl.url.split('/')[1];
    navUrl = `/${navUrl}`;

    for (const tab of this.tabs) {
      if (navUrl === tab.link) {
        this.isVisible = true;
        this.activeLink = tab.link;
      }
    }
  }

  ngOnDestroy(): void {
    this.navigationEndSubscription.unsubscribe();
    this.isLoggedInSubscription.unsubscribe();
    this.tagNameSubscription.unsubscribe();
  }
}
