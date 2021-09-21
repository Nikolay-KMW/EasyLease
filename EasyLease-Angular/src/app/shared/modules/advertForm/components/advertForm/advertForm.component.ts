import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';
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
import {select, Store} from '@ngrx/store';
import {map, shareReplay, startWith} from 'rxjs/operators';
import {Observable, Subscription} from 'rxjs';
import {MatChipInputEvent} from '@angular/material/chips';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {STEPPER_GLOBAL_OPTIONS} from '@angular/cdk/stepper';
import {DateAdapter, MatDateFormats, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';
import {Moment} from 'moment';
import * as moment from 'moment';
import 'moment/locale/ru';

import {AdvertInputInterface} from 'src/app/shared/types/advertInput.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {TagType} from 'src/app/shared/types/tag.type';
import {additionalDataSelector, isFallingSelector, isLoadingSelector} from '../../store/selectors';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {getAdditionalDataAction} from '../../store/actions/getAdditionalData.action';
import {AdvertAdditionalData} from '../../types/advertAdditionalData.interface';
import {AdvertLocation} from '../../types/advertLocation.interface';
import {PriceTypeExtended} from '../../types/priceTypeExtended.interface';
import {
  MAT_MOMENT_DATE_ADAPTER_OPTIONS,
  MAT_MOMENT_DATE_FORMATS,
  MomentDateAdapter,
} from '@angular/material-moment-adapter';
import {BreakpointObserver, Breakpoints, BreakpointState} from '@angular/cdk/layout';
import {ComfortType} from 'src/app/shared/types/comfort.type';
import {MatSelectionList} from '@angular/material/list';

export const DATE_FORMATS: MatDateFormats = {
  parse: {
    dateInput: 'DD.MM.YYYY',
  },
  display: {
    dateInput: 'DD.MM.YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'L',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'el-advert-form',
  templateUrl: './advertForm.component.html',
  styleUrls: ['./advertForm.component.scss'],
  providers: [
    {
      provide: STEPPER_GLOBAL_OPTIONS,
      useValue: {displayDefaultIndicatorType: false, showError: true},
    },
    {provide: MAT_DATE_LOCALE, useValue: 'ru-RU'},
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS],
    },
    {provide: MAT_DATE_FORMATS, useValue: DATE_FORMATS},
    {provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: {useUtc: true}},
    {provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: {strict: true}},
  ],
})
export class AdvertFormComponent implements OnInit, OnDestroy {
  @Input('initialValues') initialValuesProps: AdvertInputInterface | null = null;
  @Input('isSubmitting') isSubmittingProps: boolean = false;
  @Input('errors') errorsProps: BackendErrorInterface | null = null;

  @Output('advertSubmit') advertSubmitEvent = new EventEmitter<AdvertInputInterface>();

  isHandset$: Observable<boolean>;
  isLoading$: Observable<boolean>;
  isFalling$: Observable<boolean>;
  additionalData: AdvertAdditionalData | null = null;

  additionalDataSubscription: Subscription;

  advertTypeForm: FormGroup;
  advertTypeControl: FormControl;
  advertTypes: string[] = [];

  descriptionForm: FormGroup;
  titleControl: FormControl;
  maxTitle: number = 150;
  descriptionControl: FormControl;
  maxDescription: number = 1000;

  apartmentParametersForm: FormGroup;
  areaControl: FormControl;
  minArea: number = 1;
  maxArea: number = 100000;
  numberOfRoomsControl: FormControl;
  minNumberOfRooms: number = 1;
  maxNumberOfRooms: number = 500;
  numberOfStoreysControl: FormControl;
  minNumberOfStoreys: number = 1;
  maxNumberOfStoreys: number = 1000;
  storeyControl: FormControl;
  minStorey: number = 1;
  maxStorey: number = 1000;

  addressForm: FormGroup;
  regionControl: FormControl;
  filteredRegions: Observable<string[]>;
  districtControl: FormControl;
  filteredDistrict: Observable<string[]>;
  settlementTypeControl: FormControl;
  settlementTypes: string[] = [];
  settlementNameControl: FormControl;
  minSettlementName: number = 1;
  maxSettlementName: number = 100;
  streetTypeControl: FormControl;
  streetTypes: string[] = [];
  streetNameControl: FormControl;
  minStreetName: number = 1;
  maxStreetName: number = 150;
  houseNumberControl: FormControl;
  maxHouseNumber: number = 50;
  apartmentNumberControl: FormControl;
  maxApartmentNumber: number = 10000;

  PriceForm: FormGroup;
  priceTypeControl: FormControl;
  priceTypes: PriceTypeExtended[] = [];
  priceControl: FormControl;
  minPrice: number = 0;
  maxPrice: number = 1000000;

  DatepickerForm: FormGroup;
  startOfLeaseControl: FormControl;
  endOfLeaseControl: FormControl;
  currentDate = new Date();

  ComfortForm: FormGroup;
  comfortListControl: FormControl;
  comforts: ComfortType[] = [];
  selectedComfortList: ComfortType[] = [];

  tagForm: FormGroup;
  tagListControl: FormControl;
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

  constructor(
    private store: Store<AppStateInterface>,
    private fb: FormBuilder,
    private breakpointObserver: BreakpointObserver
  ) {
    // Initialize values
    this.isHandset$ = this.breakpointObserver.observe([Breakpoints.Handset, Breakpoints.TabletPortrait]).pipe(
      map((result: BreakpointState) => result.matches),
      shareReplay()
    );
    this.isLoading$ = this.store.pipe(select(isLoadingSelector));
    this.isFalling$ = this.store.pipe(select(isFallingSelector));

    this.additionalDataSubscription = this.store
      .pipe(select(additionalDataSelector))
      .subscribe((additionalData: AdvertAdditionalData | null) => {
        this.additionalData = additionalData;
        this.ngOnInit();
      });

    // Fetch data
    this.store.dispatch(getAdditionalDataAction());

    // Initialize Forms
    // TODO: rename advertType to immovablesType !!!!!!!!!!!
    //--------------------------------------------------------------------------------
    this.advertTypeForm = this.fb.group({
      advertType: [[], [Validators.required]],
    });

    this.advertTypeControl = this.advertTypeForm.controls['advertType'] as FormControl;

    //--------------------------------------------------------------------------------
    this.descriptionForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(this.maxTitle)]],
      description: ['', [Validators.required, Validators.maxLength(this.maxDescription)]],
    });

    this.titleControl = this.descriptionForm.controls['title'] as FormControl;
    this.descriptionControl = this.descriptionForm.controls['description'] as FormControl;

    //--------------------------------------------------------------------------------
    this.apartmentParametersForm = this.fb.group({
      area: ['', [Validators.required, Validators.min(this.minArea), Validators.max(this.maxArea)]],
      numberOfRooms: [
        '',
        [Validators.required, Validators.min(this.minNumberOfRooms), Validators.max(this.maxNumberOfRooms)],
      ],
      numberOfStoreys: [
        '',
        [Validators.required, Validators.min(this.minNumberOfStoreys), Validators.max(this.maxNumberOfStoreys)],
      ],
      storey: ['', [Validators.required, Validators.min(this.minStorey), Validators.max(this.maxStorey)]],
    });

    this.areaControl = this.apartmentParametersForm.controls['area'] as FormControl;
    this.numberOfRoomsControl = this.apartmentParametersForm.controls['numberOfRooms'] as FormControl;
    this.numberOfStoreysControl = this.apartmentParametersForm.controls['numberOfStoreys'] as FormControl;
    this.storeyControl = this.apartmentParametersForm.controls['storey'] as FormControl;

    //--------------------------------------------------------------------------------
    this.addressForm = this.fb.group({
      region: ['', [Validators.required, (control: AbstractControl) => this.corresponds(control)]],
      district: ['', [Validators.required, (control: AbstractControl) => this.correspondsToRegion(control, 'region')]],
      settlementType: [[], [Validators.required]],
      settlementName: [
        '',
        [
          Validators.required,
          Validators.minLength(this.minSettlementName),
          Validators.maxLength(this.maxSettlementName),
        ],
      ],
      streetType: [[], [Validators.required]],
      streetName: [
        '',
        [Validators.required, Validators.minLength(this.minStreetName), Validators.maxLength(this.maxStreetName)],
      ],
      houseNumber: ['', [Validators.maxLength(this.maxHouseNumber)]],
      apartmentNumber: ['', [Validators.max(this.maxApartmentNumber)]],
    });

    this.regionControl = this.addressForm.controls['region'] as FormControl;
    this.filteredRegions = this.regionControl.valueChanges.pipe(
      startWith(''),
      map((value) =>
        this.filter(
          this.additionalData?.locations.map((location) => location.region),
          value
        )
      )
    );

    this.districtControl = this.addressForm.controls['district'] as FormControl;
    this.filteredDistrict = this.districtControl.valueChanges.pipe(
      startWith(''),
      map((value) =>
        this.filter(
          this.additionalData?.locations.find((location) => location.region === this.regionControl.value)?.district,
          value
        )
      )
    );

    this.settlementTypeControl = this.addressForm.controls['settlementType'] as FormControl;
    this.settlementNameControl = this.addressForm.controls['settlementName'] as FormControl;
    this.streetTypeControl = this.addressForm.controls['streetType'] as FormControl;
    this.streetNameControl = this.addressForm.controls['streetName'] as FormControl;
    this.houseNumberControl = this.addressForm.controls['houseNumber'] as FormControl;
    this.apartmentNumberControl = this.addressForm.controls['apartmentNumber'] as FormControl;

    //--------------------------------------------------------------------------------
    this.PriceForm = this.fb.group({
      priceType: [[], [Validators.required]],
      price: ['', [Validators.required, Validators.min(this.minPrice), Validators.max(this.maxPrice)]],
    });

    this.priceTypeControl = this.PriceForm.controls['priceType'] as FormControl;
    this.priceControl = this.PriceForm.controls['price'] as FormControl;

    //--------------------------------------------------------------------------------
    this.DatepickerForm = this.fb.group({
      startOfLease: [this.currentDate, [Validators.required]],
      endOfLease: [''],
    });

    this.startOfLeaseControl = this.DatepickerForm.controls['startOfLease'] as FormControl;
    this.endOfLeaseControl = this.DatepickerForm.controls['endOfLease'] as FormControl;

    // this.startOfLeaseControl.valueChanges.subscribe(() => {
    //   const date = this.startOfLeaseControl.value as Date;
    //   console.log(moment.parseZone(date.toJSON()).local(false).format());
    // });

    // this.endOfLeaseControl.valueChanges.subscribe(() => {
    //   const date = this.endOfLeaseControl.value as Date;
    //   console.log(moment.parseZone(date.toJSON()).local(false).format());
    // });

    //--------------------------------------------------------------------------------
    this.ComfortForm = this.fb.group({
      comfortList: [this.comforts],
    });

    this.comfortListControl = this.ComfortForm.controls['comfortList'] as FormControl;

    this.comfortListControl.valueChanges.subscribe(() => {
      const data = this.comfortListControl.value as MatSelectionList;
      console.log(data);
    });

    //--------------------------------------------------------------------------------
    this.tagForm = this.fb.group({
      tagList: [this.tags],
    });

    this.tagListControl = this.tagForm.controls['tagList'] as FormControl;
    //--------------------------------------------------------------------------------
  }

  private corresponds(control: AbstractControl): ValidationErrors | null {
    return this.additionalData?.locations.find((location) => location.region === control.value)
      ? null
      : {mismatch: true};
  }

  private correspondsToRegion(control: AbstractControl, regionControl: string): ValidationErrors | null {
    const region: string = control.parent?.get(regionControl)?.value;
    const location = this.additionalData?.locations.find((location) => location.region === region);

    return location?.district.find((district) => district === control.value) ? null : {mismatch: true};
  }

  private filter(options: string[] | undefined | null, value: string): string[] {
    const filterValue = value.toLowerCase();

    if (options) {
      return options.filter((option) => option.toLowerCase().includes(filterValue));
    }
    return [];
  }

  ngOnInit(): void {
    if (this.additionalData) {
      this.advertTypes = this.additionalData.advertType;
      this.settlementTypes = this.additionalData.settlementType;
      this.streetTypes = this.additionalData.streetType;
      this.priceTypes = this.additionalData.priceType;
      this.comforts = this.additionalData.comforts;
    }

    if (this.initialValuesProps) {
      this.titleControl.setValue(this.initialValuesProps.title);
      this.descriptionControl.setValue(this.initialValuesProps.description);

      if (this.initialValuesProps.tagList) {
        this.tags = this.initialValuesProps.tagList.slice();
      }
    }
  }

  minDateEndOfLease(date: Date): Date {
    return moment.parseZone(date.toJSON()).local(false).add(1, 'days').toDate();
  }

  addTag(event: MatChipInputEvent): void {
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

  removeTag(tag: TagType): void {
    const index = this.tags.indexOf(tag);

    if (index >= 0) {
      this.tags.splice(index, 1);
    }
  }

  onSubmit(): void {
    const advertInput: AdvertInputInterface = {
      ...this.advertTypeForm.value,
      ...this.descriptionForm.value,
      ...this.apartmentParametersForm.value,
      ...this.addressForm.value,
      ...this.PriceForm.value,
      ...this.DatepickerForm.value,
      comfortList: this.selectedComfortList.slice(),
      tagList: this.tags.slice(),
    };

    console.log(advertInput);

    this.advertSubmitEvent.emit(advertInput);
  }

  ngOnDestroy(): void {
    this.additionalDataSubscription?.unsubscribe;
  }
}
