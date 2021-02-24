import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';
import {fas, faSpinner} from '@fortawesome/free-solid-svg-icons';
import {select, Store} from '@ngrx/store';
//import {request} from 'http';
import {Observable} from 'rxjs';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {CurrentUserInterface} from 'src/app/shared/types/currentUser.interface';
import {AuthService} from '../../services/auth.service';
import {registerAction} from '../../store/actions/register.action';
import {isSubmittingSelector} from '../../store/selectors';
import {RegisterRequestInterface} from '../../types/registerRequest.interface';

@Component({
  selector: 'el-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  isSubmitting$: Observable<boolean>;

  form: FormGroup;

  username: FormControl;
  email: FormControl;
  password: FormControl;
  confirmPassword: FormControl;

  minName: number = 2;
  maxName: number = 20;
  minPassword: number = 5;

  spinner = faSpinner;
  fas = fas;

  constructor(private fb: FormBuilder, private store: Store<AppStateInterface>, private authService: AuthService) {
    this.form = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(this.minName), Validators.maxLength(this.maxName)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(this.minPassword)]],
      confirmPassword: ['', [Validators.required, (control: AbstractControl) => this.confirm(control, 'password')]],
    });

    this.username = this.form.controls['username'] as FormControl;
    this.email = this.form.controls['email'] as FormControl;
    this.password = this.form.controls['password'] as FormControl;
    this.confirmPassword = this.form.controls['confirmPassword'] as FormControl;

    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector));
  }

  ngOnInit(): void {
    // this.initializeForm();
    // this.initializeValues();
  }

  // initializeValues(): void {
  //   //this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector));
  // }

  // initializeForm(): void {
  //   //console.log('initializeForm');
  // }

  onSubmit(): void {
    console.log(this.form?.value);

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
