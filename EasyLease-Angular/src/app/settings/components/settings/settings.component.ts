import {T} from '@angular/cdk/keycodes';
import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {IconDefinition} from '@fortawesome/fontawesome-svg-core';
import {faSignOutAlt, faSpinner, faUserEdit} from '@fortawesome/free-solid-svg-icons';
import {select, Store} from '@ngrx/store';
import {Observable, Subscription} from 'rxjs';
import {filter} from 'rxjs/operators';
import {logoutAction} from 'src/app/auth/store/actions/sync.action';
import {updateCurrentUserAction} from 'src/app/auth/store/actions/updateCurrentUser.action';

import {currentUserSelector} from 'src/app/auth/store/selectors';
import {setDescriptionAction, setTitleAction} from 'src/app/shared/modules/banner/store/action/sync.action';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {CurrentUserInterface} from 'src/app/shared/types/currentUser.interface';
import {CurrentUserInputInterface} from 'src/app/shared/types/currentUserInput.interface';
import {environment} from 'src/environments/environment';
import {isSubmittingSelector, validationErrorsSelector} from '../../store/selectors';

@Component({
  selector: 'el-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent implements OnInit, OnDestroy {
  currentUser: CurrentUserInterface | null = null;
  currentUserSubscription: Subscription;

  form: FormGroup;

  image: FormControl;
  username: FormControl;
  bio: FormControl;
  email: FormControl;
  password: FormControl;

  minName: number = environment.minUserName;
  maxName: number = environment.maxUserName;
  maxBio: number = 100;
  minPassword: number = environment.minUserPassword;

  faSpinner: IconDefinition = faSpinner;
  faUserEdit: IconDefinition = faUserEdit;
  faSignOutAlt: IconDefinition = faSignOutAlt;

  isSubmitting$: Observable<boolean>;
  backendErrors$: Observable<BackendErrorInterface | null>;

  constructor(private fb: FormBuilder, private store: Store<AppStateInterface>) {
    // Initialize form
    this.form = this.fb.group({
      image: [null],
      username: ['', [Validators.required, Validators.minLength(this.minName), Validators.maxLength(this.maxName)]],
      bio: ['', [Validators.maxLength(this.maxBio)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.minLength(this.minPassword)]],
    });

    // Initialize values
    this.image = this.form.controls['image'] as FormControl;
    this.username = this.form.controls['username'] as FormControl;
    this.bio = this.form.controls['bio'] as FormControl;
    this.email = this.form.controls['email'] as FormControl;
    this.password = this.form.controls['password'] as FormControl;

    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector));
    this.backendErrors$ = this.store.pipe(select(validationErrorsSelector));

    // Initialize Listeners
    this.currentUserSubscription = this.store
      .pipe(
        select(currentUserSelector),
        filter((currentUser): currentUser is CurrentUserInterface => Boolean(currentUser)) //(!!currentUser) or (currentUser !== null && currentUser !== undefined)
      )
      .subscribe((currentUser: CurrentUserInterface) => {
        this.currentUser = currentUser;

        this.image.setValue(this.currentUser.image);
        this.username.setValue(this.currentUser.userName);
        this.bio.setValue(this.currentUser.bio);
        this.email.setValue(this.currentUser.email);
      });
  }

  ngOnInit(): void {
    this.setValueBannerModule();
  }

  setValueBannerModule(): void {
    this.store.dispatch(setTitleAction({title: 'Настройки'}));
    this.store.dispatch(setDescriptionAction({description: 'Вы можете здесь изменять настройки Вашего профиля!'}));
  }

  onSubmit(): void {
    const currentUserInput: CurrentUserInputInterface = {
      ...this.currentUser,
      ...this.form.value,
    };

    this.store.dispatch(updateCurrentUserAction({currentUserInput}));
  }

  logout(): void {
    this.store.dispatch(logoutAction());
  }

  ngOnDestroy(): void {
    this.currentUserSubscription.unsubscribe();
  }
}
