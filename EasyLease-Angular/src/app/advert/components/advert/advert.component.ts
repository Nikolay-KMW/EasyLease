import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {select, Store} from '@ngrx/store';
import {combineLatest, Observable, Subscription} from 'rxjs';
import {map} from 'rxjs/operators';
import {IconDefinition} from '@fortawesome/fontawesome-svg-core';
import {MatCarouselSlide, MatCarouselSlideComponent} from 'ng-mat-carousel';
//import {hammerjs as hammerJS} from 'hammerjs.js';
//import * as Hammer from 'hammerjs';
import 'hammerjs';

import {currentUserSelector} from 'src/app/auth/store/selectors';
import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {CurrentUserInterface} from 'src/app/shared/types/currentUser.interface';
import {getAdvertAction} from '../../store/actions/getAdvert.action';
import {advertSelector, errorSelector, isLoadingSelector} from '../../store/selectors';
import {deleteAdvertAction} from '../../store/actions/deleteAdvert.action';
import {faEdit, faTrash} from '@fortawesome/free-solid-svg-icons';
import {setDescriptionAction, setTitleAction} from 'src/app/shared/modules/banner/store/action/sync.action';

@Component({
  selector: 'el-advert',
  templateUrl: './advert.component.html',
  styleUrls: ['./advert.component.scss'],
})
export class AdvertComponent implements OnInit, OnDestroy {
  slug: string | null;
  advert: AdvertInterface | null = null;
  advertSubscription: Subscription;

  isLoading$: Observable<boolean>;
  error$: Observable<string | null>;
  isAuthor$: Observable<boolean>;

  faTrash: IconDefinition = faTrash;
  faEdit: IconDefinition = faEdit;

  slides = [
    {image: 'assets/img/city-profile.jpg'},
    {image: 'assets/img/bg2.jpg'},
    {image: 'assets/img/city.jpg'},
    {image: 'assets/img/cover.jpg'},
    {image: 'assets/img/sidebar-1.jpg'},
  ];

  constructor(private store: Store<AppStateInterface>, private route: ActivatedRoute) {
    // Initialize values
    this.slug = this.route.snapshot.paramMap.get('slug');
    this.isLoading$ = this.store.pipe(select(isLoadingSelector));
    // TODO: implements errors
    this.error$ = this.store.pipe(select(errorSelector));
    this.isAuthor$ = combineLatest([
      this.store.pipe(select(advertSelector)),
      this.store.pipe(select(currentUserSelector)),
    ]).pipe(
      map(([advert, currentUser]: [AdvertInterface | null, CurrentUserInterface | null]) => {
        if (!advert || !currentUser) {
          return false;
        }
        return currentUser.username === advert.author.username;
      })
    );

    // Fetch date
    if (this.slug) {
      this.store.dispatch(getAdvertAction({slug: this.slug}));
    }

    // Initialize listeners
    this.advertSubscription = this.store
      .pipe(select(advertSelector))
      .subscribe((advert: AdvertInterface | null) => (this.advert = advert));
  }

  ngOnInit(): void {
    this.setValueBannerModule();
  }

  setValueBannerModule(): void {
    this.store.dispatch(setTitleAction({title: 'Объявление'}));
    this.store.dispatch(
      setDescriptionAction({
        description: 'Здесь Ви можете сделать ставку',
      })
    );
  }

  deleteAdvert(): void {
    if (this.slug) {
      this.store.dispatch(deleteAdvertAction({slug: this.slug}));
    }
  }

  ngOnDestroy(): void {
    this.advertSubscription.unsubscribe();
  }
}
