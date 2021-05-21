import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {faSignInAlt, faSpinner, faUserPlus, IconDefinition} from '@fortawesome/free-solid-svg-icons';
import {select, Store} from '@ngrx/store';
import {Observable} from 'rxjs';
import {setDescriptionAction, setTitleAction} from 'src/app/shared/modules/banner/store/action/sync.action';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {environment} from 'src/environments/environment';
import {AuthService} from '../../services/auth.service';
import {loginAction} from '../../store/actions/login.action';
import {isSubmittingSelector, validationErrorsSelector} from '../../store/selectors';
import {LoginRequestInterface} from '../../types/loginRequest.interface';

@Component({
  selector: 'el-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  isSubmitting$: Observable<boolean>;
  backendErrors$: Observable<BackendErrorInterface | null>;

  form: FormGroup;

  email: FormControl;
  password: FormControl;

  minPassword: number = environment.minUserPassword;

  faSpinner: IconDefinition = faSpinner;
  faSignInAlt: IconDefinition = faSignInAlt;
  faUserPlus: IconDefinition = faUserPlus;

  constructor(private fb: FormBuilder, private store: Store<AppStateInterface>, private authService: AuthService) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(this.minPassword)]],
    });

    this.email = this.form.controls['email'] as FormControl;
    this.password = this.form.controls['password'] as FormControl;

    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector));
    this.backendErrors$ = this.store.pipe(select(validationErrorsSelector));
  }

  ngOnInit(): void {
    this.setValueBannerModule();
  }

  setValueBannerModule(): void {
    this.store.dispatch(setTitleAction({title: 'Вход'}));
    this.store.dispatch(
      setDescriptionAction({
        description: 'Введите данные для входа в систему',
      })
    );
  }

  onSubmit(): void {
    const request: LoginRequestInterface = {
      user: this.form.value,
    };
    this.store.dispatch(loginAction({request}));
  }
}
