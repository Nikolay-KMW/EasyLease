import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {select, Store} from '@ngrx/store';
import {Subscription} from 'rxjs';
import {filter} from 'rxjs/operators';

import {currentUserSelector} from 'src/app/auth/store/selectors';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {CurrentUserInterface} from 'src/app/shared/types/currentUser.interface';

@Component({
  selector: 'el-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent implements OnInit, OnDestroy {
  //currentUser: CurrentUserInterface | null = null;
  //currentUserSubscription: Subscription;

  constructor(private fb: FormBuilder, private store: Store<AppStateInterface>) {
    // Initialize form
    //     this.form = this.fb.group({
    //       username: ['', [Validators.required, Validators.minLength(this.minName), Validators.maxLength(this.maxName)]],
    //       email: ['', [Validators.required, Validators.email]],
    //       password: ['', [Validators.required, Validators.minLength(this.minPassword)]],
    //       confirmPassword: ['', [Validators.required, (control: AbstractControl) => this.confirm(control, 'password')]],
    //     });
    //     // Initialize values
    //     this.username = this.form.controls['username'] as FormControl;
    //     this.email = this.form.controls['email'] as FormControl;
    //     this.password = this.form.controls['password'] as FormControl;
    //     this.confirmPassword = this.form.controls['confirmPassword'] as FormControl;
    //     this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector));
    //     this.backendErrors$ = this.store.pipe(select(validationErrorsSelector));
    // Initialize Listeners
    // this.currentUserSubscription = this.store
    //   .pipe(select(currentUserSelector), filter(Boolean))
    //   .subscribe((currentUser: CurrentUserInterface) => {
    //     this.currentUser = currentUser;
    //   });
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }
}
