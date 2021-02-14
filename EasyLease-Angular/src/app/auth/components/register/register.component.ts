import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'el-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  form: FormGroup;
  emailFormControl: FormControl;

  constructor(private fb: FormBuilder) {

    this.form = this.fb.group({
      username: ['', Validators.required],
      email: '',
      password: '',
    });

    this.emailFormControl = new FormControl('', [
      Validators.required,
      Validators.email,
    ]);
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    console.log('initializeForm');
  }

  onSubmit(): void {
    console.log(this.form?.value);
  }
}
