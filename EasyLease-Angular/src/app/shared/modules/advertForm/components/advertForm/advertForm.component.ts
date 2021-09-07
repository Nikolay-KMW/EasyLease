import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {IconDefinition} from '@fortawesome/fontawesome-svg-core';
import {
  faCheck,
  faExclamationTriangle,
  faFileAlt,
  faFlagCheckered,
  faHashtag,
  faPenAlt,
  faSpinner,
  faStepBackward,
  faStepForward,
  faTimes,
  faTimesCircle,
  faTrademark,
} from '@fortawesome/free-solid-svg-icons';
import {MatChipInputEvent} from '@angular/material/chips';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {STEPPER_GLOBAL_OPTIONS} from '@angular/cdk/stepper';

import {AdvertInputInterface} from 'src/app/shared/types/advertInput.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {TagType} from 'src/app/shared/types/tag.type';

@Component({
  selector: 'el-advert-form',
  templateUrl: './advertForm.component.html',
  styleUrls: ['./advertForm.component.scss'],
  providers: [
    {
      provide: STEPPER_GLOBAL_OPTIONS,
      useValue: {displayDefaultIndicatorType: false, showError: true},
    },
  ],
})
export class AdvertFormComponent implements OnInit {
  @Input('initialValues') initialValuesProps: AdvertInputInterface | null = null;
  @Input('isSubmitting') isSubmittingProps: boolean = false;
  @Input('errors') errorsProps: BackendErrorInterface | null = null;

  @Output('advertSubmit') advertSubmitEvent = new EventEmitter<AdvertInputInterface>();

  descriptionForm: FormGroup;
  bodyForm: FormGroup;
  tagListForm: FormGroup;
  maxTitle: number = 20;
  maxDescription: number = 100;

  title: FormControl;
  description: FormControl;
  body: FormControl;
  tagList: FormControl;

  tags: TagType[] = [];

  visible: boolean = true;
  selectable: boolean = true;
  removable: boolean = true;
  addOnBlur: boolean = true;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];

  faStepForward: IconDefinition = faStepForward;
  faStepBackward: IconDefinition = faStepBackward;
  faSpinner: IconDefinition = faSpinner;
  faFlagCheckered: IconDefinition = faFlagCheckered;
  faTimes: IconDefinition = faTimes;
  faTimesCircle: IconDefinition = faTimesCircle;
  faPenAlt: IconDefinition = faPenAlt;
  faCheck: IconDefinition = faCheck;
  faExclamationTriangle: IconDefinition = faExclamationTriangle;
  faTradeMark: IconDefinition = faTrademark;
  faFileAlt: IconDefinition = faFileAlt;
  faHashTag: IconDefinition = faHashtag;

  constructor(private fb: FormBuilder) {
    // Initialize forms
    this.descriptionForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(this.maxTitle)]],
      description: ['', [Validators.required, Validators.maxLength(this.maxDescription)]],
    });
    this.bodyForm = this.fb.group({
      body: ['', [Validators.required]],
    });
    this.tagListForm = this.fb.group({
      tagList: [this.tags],
    });

    // Initialize values
    this.title = this.descriptionForm.controls['title'] as FormControl;
    this.description = this.descriptionForm.controls['description'] as FormControl;
    this.body = this.bodyForm.controls['body'] as FormControl;
    this.tagList = this.tagListForm.controls['tagList'] as FormControl;
  }

  ngOnInit(): void {
    if (this.initialValuesProps) {
      this.title.setValue(this.initialValuesProps.title);
      this.description.setValue(this.initialValuesProps.description);

      if (this.initialValuesProps.tagList) {
        this.tags = this.initialValuesProps.tagList.slice();
      }
    }
  }

  add(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our tag
    if ((value || '').trim()) {
      this.tags.push(value.trim());
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(tag: TagType): void {
    const index = this.tags.indexOf(tag);

    if (index >= 0) {
      this.tags.splice(index, 1);
    }
  }

  onSubmit(): void {
    const advertInput: AdvertInputInterface = {
      ...this.descriptionForm.value,
      ...this.bodyForm.value,
      tagList: this.tags.slice(),
    };

    //console.log(advertInput);

    this.advertSubmitEvent.emit(advertInput);
  }
}
