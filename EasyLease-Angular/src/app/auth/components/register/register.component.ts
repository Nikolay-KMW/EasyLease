import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';
import {faSignInAlt, faSpinner, faUserPlus, IconDefinition} from '@fortawesome/free-solid-svg-icons';
import {select, Store} from '@ngrx/store';
import {Observable} from 'rxjs';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {environment} from 'src/environments/environment';
import {AuthService} from '../../services/auth.service';
import {registerAction} from '../../store/actions/register.action';
import {isSubmittingSelector, validationErrorsSelector} from '../../store/selectors';
import {RegisterRequestInterface} from '../../types/registerRequest.interface';

@Component({
  selector: 'el-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  isSubmitting$: Observable<boolean>;
  backendErrors$: Observable<BackendErrorInterface | null>;

  form: FormGroup;

  username: FormControl;
  email: FormControl;
  password: FormControl;
  confirmPassword: FormControl;

  minName: number = environment.minUserName;
  maxName: number = environment.maxUserName;
  minPassword: number = environment.minUserPassword;

  faSpinner: IconDefinition = faSpinner;
  faSignInAlt: IconDefinition = faSignInAlt;
  faUserPlus: IconDefinition = faUserPlus;

  constructor(private fb: FormBuilder, private store: Store<AppStateInterface>, private authService: AuthService) {
    // Initialize form
    this.form = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(this.minName), Validators.maxLength(this.maxName)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(this.minPassword)]],
      confirmPassword: ['', [Validators.required, (control: AbstractControl) => this.confirm(control, 'password')]],
    });

    // Initialize values
    this.username = this.form.controls['username'] as FormControl;
    this.email = this.form.controls['email'] as FormControl;
    this.password = this.form.controls['password'] as FormControl;
    this.confirmPassword = this.form.controls['confirmPassword'] as FormControl;

    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector));
    this.backendErrors$ = this.store.pipe(select(validationErrorsSelector));
  }

  ngOnInit(): void {}

  onSubmit(): void {
    const request: RegisterRequestInterface = {
      user: this.form.value,
    };
    this.store.dispatch(registerAction({request}));
  }

  confirm(control: AbstractControl, matchPassword: string): ValidationErrors | null {
    return control.parent?.get(matchPassword)?.value === control.value ? null : {mismatch: true};
  }
}

// export class ConfirmValidators {
//   static confirm(control: AbstractControl, matchPassword: string): ValidationErrors | null {
//     return control.parent?.get(matchPassword)?.value === control.value ? null : {mismatch: true};
//   }
// }
