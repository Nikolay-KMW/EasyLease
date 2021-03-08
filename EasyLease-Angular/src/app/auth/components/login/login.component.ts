import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {faSignInAlt, faSpinner, faUser, faUserPlus, IconDefinition} from '@fortawesome/free-solid-svg-icons';
import {select, Store} from '@ngrx/store';
import {Observable} from 'rxjs';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {AuthService} from '../../services/auth.service';
import {loginAction} from '../../store/actions/login.action';
import {isSubmittingSelector, validationErrorsSelected} from '../../store/selectors';
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

  minPassword: number = 8;

  faSpinner: IconDefinition = faSpinner;
  faUser: IconDefinition = faUser;
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
    this.backendErrors$ = this.store.pipe(select(validationErrorsSelected));
  }

  ngOnInit(): void {
    // this.initializeForm();
    // this.initializeValues();
  }

  onSubmit(): void {
    //console.log(this.form?.value);

    const request: LoginRequestInterface = {
      user: this.form.value,
    };
    this.store.dispatch(loginAction({request}));
  }
}
