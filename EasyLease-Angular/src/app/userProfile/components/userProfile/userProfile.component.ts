import {Component, OnDestroy, OnInit} from '@angular/core';
import {DomSanitizer, SafeResourceUrl} from '@angular/platform-browser';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {IconDefinition} from '@fortawesome/fontawesome-svg-core';
import {faUserCog} from '@fortawesome/free-solid-svg-icons';
import {select, Store} from '@ngrx/store';
import {combineLatest, Observable, Subscription} from 'rxjs';
import {filter, map} from 'rxjs/operators';
import {currentUserSelector} from 'src/app/auth/store/selectors';
import {setDescriptionAction, setTitleAction} from 'src/app/shared/modules/banner/store/action/sync.action';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {ProfileInterface} from 'src/app/shared/types/profile.Interface';
import {getUserProfileAction} from '../../store/action/getUserProfile.action';
import {errorSelector, isLoadingSelector, userProfileSelector} from '../../store/selectors';

@Component({
  selector: 'el-user-profile',
  templateUrl: './userProfile.component.html',
  styleUrls: ['./userProfile.component.scss'],
})
export class UserProfileComponent implements OnInit, OnDestroy {
  userProfile: ProfileInterface | null = null;
  isLoading$: Observable<boolean>;
  error$: Observable<string | null>;
  userProfileSubscription: Subscription;
  routeParamsSubscription: Subscription;
  slug: string | null;
  isCurrentUserProfile$: Observable<boolean>;
  apiUrl: string;

  bypassSecurityTrust: (value: string) => SafeResourceUrl;

  faUserCog: IconDefinition = faUserCog;

  constructor(
    private store: Store<AppStateInterface>,
    private route: ActivatedRoute,
    private router: Router,
    private sanitizer: DomSanitizer
  ) {
    this.bypassSecurityTrust = this.sanitizer.bypassSecurityTrustResourceUrl;

    this.slug = this.route.snapshot.paramMap.get('slug');

    this.isLoading$ = this.store.pipe(select(isLoadingSelector));
    this.error$ = this.store.pipe(select(errorSelector));

    this.apiUrl = this.getApiUrl();

    this.routeParamsSubscription = this.route.params.subscribe((params: Params) => {
      this.slug = params.slug;
      this.fetchCurrentUser();
      this.apiUrl = this.getApiUrl();
    });

    this.userProfileSubscription = this.store
      .pipe(select(userProfileSelector))
      .subscribe((userProfile: ProfileInterface | null) => (this.userProfile = userProfile));

    this.isCurrentUserProfile$ = combineLatest([
      this.store.pipe(
        select(currentUserSelector),
        filter((currentUser) => Boolean(currentUser))
      ),
      this.store.pipe(
        select(userProfileSelector),
        filter((userProfile) => Boolean(userProfile))
      ),
    ]).pipe(map(([currentUser, userProfile]) => currentUser?.id === userProfile?.id));
  }

  ngOnInit(): void {
    this.setValueBannerModule();
    this.fetchCurrentUser();
  }

  setValueBannerModule(): void {
    this.store.dispatch(setTitleAction({title: `Профиль пользователя`}));
    this.store.dispatch(
      setDescriptionAction({
        description: 'Вы можете просматривать объявления пользователя и отзывы о нем',
      })
    );
  }

  fetchCurrentUser(): void {
    if (this.slug) {
      this.store.dispatch(getUserProfileAction({slug: this.slug}));
    }
  }

  getApiUrl(): string {
    return `/profile/${this.slug}/adverts`;

    // const isComments = this.router.url.includes('favorites');
    // return isComments ? `/articles?favorited=${this.slug}` : `/articles?author=${this.slug}`;
  }

  ngOnDestroy(): void {
    this.userProfileSubscription.unsubscribe();
    this.routeParamsSubscription.unsubscribe();
  }
}
