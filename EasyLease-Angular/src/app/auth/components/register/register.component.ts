import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';
import {Store} from '@ngrx/store';
import {registerAction} from '../../store/actions/register.action';

@Component({
  selector: 'el-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  form: FormGroup;

  username: FormControl;
  email: FormControl;
  password: FormControl;
  confirmPassword: FormControl;

  minName: number = 2;
  maxName: number = 50;
  minPassword: number = 5;

  constructor(private fb: FormBuilder, private store: Store) {
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
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    console.log('initializeForm');
  }

  onSubmit(): void {
    console.log(this.form?.value);
    this.store.dispatch(registerAction(this.form.value));
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
