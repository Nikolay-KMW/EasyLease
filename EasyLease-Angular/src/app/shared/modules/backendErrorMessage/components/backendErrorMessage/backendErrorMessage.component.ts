import {Component, Input} from '@angular/core';
import {OnInit} from '@angular/core';
import {FormControl} from '@angular/forms';

import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {environment} from 'src/environments/environment';

@Component({
  selector: 'el-backend-error-message',
  templateUrl: './backendErrorMessage.component.html',
  styleUrls: ['./backendErrorMessage.component.scss'],
})
export class BackendErrorMessageComponent implements OnInit {
  @Input('backendErrors') backendErrorsProps?: BackendErrorInterface;
  @Input('namePropertyBackendError') namePropertyProps?: string;
  @Input('formControlForBackendError') formControlProps?: FormControl;

  errorMessage: string[] | undefined;
  show: boolean = true;

  constructor() {}

  ngOnInit(): void {
    this.show = true;

    if (this.backendErrorsProps && this.namePropertyProps) {
      //this.errorMessage = this.backendErrorsProps[this.namePropertyProps]?.join(', ');
      this.errorMessage = this.backendErrorsProps[this.namePropertyProps];
    }

    if (this.errorMessage !== undefined) {
      setTimeout(() => this.formControlProps?.setErrors({backendErrorMessage: true}));

      setTimeout(() => (this.show = false), environment.timeShowBackendError);

      // setTimeout(() => {
      //   this.formControlProps?.setErrors({backendErrorMessage: true});
      //   let formGroup = this.formControlProps?.parent;

      //   console.log(formGroup);

      //   formGroup?.setErrors({backendErrorMessage: true});
      // });
    }

    // Object.keys(this.backendErrorsProps as BackendErrorInterface).map((name: string) => {
    //     const message = (this.backendErrorsProps as BackendErrorInterface)[name].join(', ');
    //     console.log('errorMessage :');
    //     return `${name} ${message}`;
    //   });
  }
}
