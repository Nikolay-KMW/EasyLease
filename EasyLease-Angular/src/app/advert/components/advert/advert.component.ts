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
import {advertSelector, errorSelector, isFallingSelector, isLoadingSelector} from '../../store/selectors';
import {deleteAdvertAction} from '../../store/actions/deleteAdvert.action';
import {faEdit, faTrash} from '@fortawesome/free-solid-svg-icons';
import {setDescriptionAction, setTitleAction} from 'src/app/shared/modules/banner/store/action/sync.action';
import {PriceType} from 'src/app/shared/types/price.type';
import {DomSanitizer, SafeResourceUrl} from '@angular/platform-browser';
import {ImagePathType} from 'src/app/shared/types/imagePath.type';
import {environment} from 'src/environments/environment';

@Component({
  selector: 'el-advert',
  templateUrl: './advert.component.html',
  styleUrls: ['./advert.component.scss'],
})
export class AdvertComponent implements OnInit, OnDestroy {
  //PriceType = PriceType;
  priceType: string = PriceType.PricePerMonth;

  slug: string | null;
  advert: AdvertInterface | null = null;
  advertSubscription: Subscription;

  bypassSecurityTrust: (value: string) => SafeResourceUrl;

  isLoading$: Observable<boolean>;
  error$: Observable<string | null>;
  isFalling$: Observable<boolean>;
  isAuthor$: Observable<boolean>;

  faTrash: IconDefinition = faTrash;
  faEdit: IconDefinition = faEdit;

  slides = [{image: 'assets/img/apartment.png'}];

  constructor(private store: Store<AppStateInterface>, private route: ActivatedRoute, private sanitizer: DomSanitizer) {
    // Initialize values
    this.bypassSecurityTrust = this.sanitizer.bypassSecurityTrustResourceUrl;
    this.slug = this.route.snapshot.paramMap.get('slug');
    this.isLoading$ = this.store.pipe(select(isLoadingSelector));

    // TODO: implements errors
    this.error$ = this.store.pipe(select(errorSelector));
    this.isFalling$ = this.store.pipe(select(isFallingSelector));

    this.isAuthor$ = combineLatest([
      this.store.pipe(select(advertSelector)),
      this.store.pipe(select(currentUserSelector)),
    ]).pipe(
      map(([advert, currentUser]: [AdvertInterface | null, CurrentUserInterface | null]) => {
        if (!advert || !currentUser) {
          return false;
        }
        return currentUser.userName === advert.author?.userName;
      })
    );

    // Fetch date
    if (this.slug) {
      this.store.dispatch(getAdvertAction({slug: this.slug}));
    }

    // Initialize listeners
    this.advertSubscription = this.store.pipe(select(advertSelector)).subscribe((advert: AdvertInterface | null) => {
      this.advert = advert;
      if (advert) {
        this.priceType = PriceType[advert.priceType as string as keyof typeof PriceType];
        if (advert.images.length > 0) {
          this.slides = this.getSlideImage(advert.images);
        }
      }
    });
  }

  // GetValue = (valueKey: string): string => {
  //   const key = Object.keys(PriceType).find((key) => key === valueKey);
  //   return PriceType[key as keyof typeof PriceType];
  // };

  getSlideImage(imagePaths: ImagePathType[]): {image: string}[] {
    let slides: {image: string}[] = [];

    for (let index = 0; index < imagePaths.length; index++) {
      const slide = environment.uploadUrl + imagePaths[index];
      slides[index] = {image: slide};
    }
    return slides;
  }

  ngOnInit(): void {
    this.setValueBannerModule();
  }

  setValueBannerModule(): void {
    this.store.dispatch(setTitleAction({title: 'Объявление'}));
    this.store.dispatch(
      setDescriptionAction({
        description: 'Здесь Ви можете предложить свою цену',
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
