import {Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';
import {IconDefinition} from '@fortawesome/fontawesome-svg-core';
import {
  faBuilding,
  faCalendarAlt,
  faCheck,
  faCouch,
  faExclamationTriangle,
  faFileAlt,
  faFlagCheckered,
  faHashtag,
  faHryvnia,
  faLayerGroup,
  faMapMarkedAlt,
  faPenAlt,
  faSpinner,
  faStepBackward,
  faStepForward,
  faTimes,
  faTimesCircle,
} from '@fortawesome/free-solid-svg-icons';
import {select, Store} from '@ngrx/store';
import {filter, map, shareReplay, startWith} from 'rxjs/operators';
import {Observable, Subscription} from 'rxjs';
import {MatChipInputEvent, MatChipList} from '@angular/material/chips';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {STEPPER_GLOBAL_OPTIONS} from '@angular/cdk/stepper';
import {DateAdapter, MatDateFormats, MAT_DATE_FORMATS, MAT_DATE_LOCALE} from '@angular/material/core';
import {BreakpointObserver, Breakpoints, BreakpointState} from '@angular/cdk/layout';
import {MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter} from '@angular/material-moment-adapter';
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
import {PriceTypeExtended} from '../../types/priceTypeExtended.interface';
import {ComfortType} from 'src/app/shared/types/comfort.type';
import {environment} from 'src/environments/environment';
import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {MatSnackBar} from '@angular/material/snack-bar';

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
  @Input('initialValues') initialValuesProps: AdvertInterface | null = null;
  @Input('isSubmitting') isSubmittingProps: boolean = false;
  @Input('backendErrors') backendErrorsProps: Observable<BackendErrorInterface | null> = new Observable<null>();
  errorsProps: BackendErrorInterface | null = null;

  backendErrorsPropsSubscription: Subscription | null = null;

  @Output('advertSubmit') advertSubmitEvent = new EventEmitter<AdvertInputInterface>();

  isHandset$: Observable<boolean>;
  isLoading$: Observable<boolean>;
  isFalling$: Observable<boolean>;
  additionalData: AdvertAdditionalData | null = null;

  additionalDataSubscription: Subscription;

  realtyTypeForm: FormGroup;
  realtyTypeControl: FormControl;
  realtyTypes: string[] = [];

  descriptionForm: FormGroup;
  titleControl: FormControl;
  maxTitle: number = environment.maxTitle;
  descriptionControl: FormControl;
  maxDescription: number = environment.maxDescription;

  apartmentParametersForm: FormGroup;
  areaControl: FormControl;
  minArea: number = environment.minArea;
  maxArea: number = environment.maxArea;
  numberOfRoomsControl: FormControl;
  minNumberOfRooms: number = environment.minNumberOfRooms;
  maxNumberOfRooms: number = environment.maxNumberOfRooms;
  numberOfStoreysControl: FormControl;
  minNumberOfStoreys: number = environment.minNumberOfStoreys;
  maxNumberOfStoreys: number = environment.maxNumberOfStoreys;
  storeyControl: FormControl;
  minStorey: number = environment.minStorey;
  maxStorey: number = environment.maxStorey;

  addressForm: FormGroup;
  regionControl: FormControl;
  filteredRegions: Observable<string[]>;
  districtControl: FormControl;
  filteredDistrict: Observable<string[]>;
  settlementTypeControl: FormControl;
  settlementTypes: string[] = [];
  settlementNameControl: FormControl;
  minSettlementName: number = environment.minSettlementName;
  maxSettlementName: number = environment.maxSettlementName;
  streetTypeControl: FormControl;
  streetTypes: string[] = [];
  streetNameControl: FormControl;
  minStreetName: number = environment.minStreetName;
  maxStreetName: number = environment.maxStreetName;
  houseNumberControl: FormControl;
  maxHouseNumber: number = environment.maxHouseNumber;
  apartmentNumberControl: FormControl;
  maxApartmentNumber: number = environment.maxApartmentNumber;

  priceForm: FormGroup;
  priceTypeControl: FormControl;
  priceTypes: PriceTypeExtended[] = [];
  priceControl: FormControl;
  minPrice: number = environment.minPrice;
  maxPrice: number = environment.maxPrice;

  datepickerForm: FormGroup;
  startOfLeaseControl: FormControl;
  endOfLeaseControl: FormControl;
  currentDate: Date = moment
    .parseZone(new Date().toUTCString())
    .add(1, 'days')
    .add(environment.hoursOffsetForUkraine, 'hours')
    .local(true)
    .toDate();

  comfortForm: FormGroup;
  comfortListControl: FormControl;
  comforts: ComfortType[] = [];
  selectedComfortList: ComfortType[] = [];

  @ViewChild('tagList') tagList: MatChipList | null = null;
  tagForm: FormGroup;
  tagListControl: FormControl;
  maxTag: number = environment.maxTag;
  tagListLimit: number = environment.tagListLimit;
  tags: TagType[] = [];
  visibleTag: boolean = true;
  selectableTag: boolean = true;
  removableTag: boolean = true;
  addOnBlurForTag: boolean = true;
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

  faBuilding: IconDefinition = faBuilding;
  faFileAlt: IconDefinition = faFileAlt;
  faLayerGroup: IconDefinition = faLayerGroup;
  faMapMarkedAlt: IconDefinition = faMapMarkedAlt;
  faHryvnia: IconDefinition = faHryvnia;
  faCalendarAlt: IconDefinition = faCalendarAlt;
  faCouch: IconDefinition = faCouch;
  faHashTag: IconDefinition = faHashtag;

  constructor(
    private store: Store<AppStateInterface>,
    private fb: FormBuilder,
    private breakpointObserver: BreakpointObserver,
    private snackBar: MatSnackBar
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
    //--------------------------------------------------------------------------------
    this.realtyTypeForm = this.fb.group({
      realtyType: [null, [Validators.required]],
    });

    this.realtyTypeControl = this.realtyTypeForm.controls['realtyType'] as FormControl;

    //--------------------------------------------------------------------------------
    this.descriptionForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(this.maxTitle)]],
      description: ['', [Validators.required, Validators.maxLength(this.maxDescription)]],
    });

    this.titleControl = this.descriptionForm.controls['title'] as FormControl;
    this.descriptionControl = this.descriptionForm.controls['description'] as FormControl;

    //--------------------------------------------------------------------------------
    this.apartmentParametersForm = this.fb.group({
      area: [0, [Validators.required, Validators.min(this.minArea), Validators.max(this.maxArea)]],
      numberOfRooms: [
        0,
        [Validators.required, Validators.min(this.minNumberOfRooms), Validators.max(this.maxNumberOfRooms)],
      ],
      numberOfStoreys: [
        0,
        [Validators.required, Validators.min(this.minNumberOfStoreys), Validators.max(this.maxNumberOfStoreys)],
      ],
      storey: [0, [Validators.required, Validators.min(this.minStorey), Validators.max(this.maxStorey)]],
    });

    this.areaControl = this.apartmentParametersForm.controls['area'] as FormControl;
    this.numberOfRoomsControl = this.apartmentParametersForm.controls['numberOfRooms'] as FormControl;
    this.numberOfStoreysControl = this.apartmentParametersForm.controls['numberOfStoreys'] as FormControl;
    this.storeyControl = this.apartmentParametersForm.controls['storey'] as FormControl;

    //--------------------------------------------------------------------------------
    this.addressForm = this.fb.group({
      region: ['', [Validators.required, (control: AbstractControl) => this.corresponds(control)]],
      district: ['', [Validators.required, (control: AbstractControl) => this.correspondsToRegion(control, 'region')]],
      settlementType: [null, [Validators.required]],
      settlementName: [
        '',
        [
          Validators.required,
          Validators.minLength(this.minSettlementName),
          Validators.maxLength(this.maxSettlementName),
        ],
      ],
      streetType: [null, [Validators.required]],
      streetName: [
        '',
        [Validators.required, Validators.minLength(this.minStreetName), Validators.maxLength(this.maxStreetName)],
      ],
      houseNumber: [null, [Validators.maxLength(this.maxHouseNumber)]],
      apartmentNumber: [null, [Validators.max(this.maxApartmentNumber)]],
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
    this.priceForm = this.fb.group({
      priceType: [null, [Validators.required]],
      price: [0, [Validators.required, Validators.min(this.minPrice), Validators.max(this.maxPrice)]],
    });

    this.priceTypeControl = this.priceForm.controls['priceType'] as FormControl;
    this.priceControl = this.priceForm.controls['price'] as FormControl;

    //--------------------------------------------------------------------------------
    this.datepickerForm = this.fb.group({
      startOfLease: [this.currentDate, [Validators.required]],
      endOfLease: [null],
    });

    this.startOfLeaseControl = this.datepickerForm.controls['startOfLease'] as FormControl;
    this.endOfLeaseControl = this.datepickerForm.controls['endOfLease'] as FormControl;

    // console.log('currentDate', this.currentDate);

    // this.startOfLeaseControl.valueChanges.subscribe(() => {
    //   const date = this.startOfLeaseControl.value as Date;
    //   console.log(date.toString());

    //   //console.log(moment.parseZone(date.toJSON()).local(false).format());
    // });

    // this.endOfLeaseControl.valueChanges.subscribe(() => {
    //   const date = this.endOfLeaseControl.value as Date;

    //   if (date) {
    //     console.log(date.toString());
    //     //console.log(moment.parseZone(date.toJSON()).local(false).format());
    //   }
    // });

    //--------------------------------------------------------------------------------
    this.comfortForm = this.fb.group({
      comfortList: [this.comforts],
    });

    this.comfortListControl = this.comfortForm.controls['comfortList'] as FormControl;

    //--------------------------------------------------------------------------------
    this.tagForm = this.fb.group({
      tagList: [
        this.tags,
        [
          (control: AbstractControl) => this.maxTagLength(control, this.maxTag),
          (control: AbstractControl) => this.backendErrorForTags(control),
        ],
      ],
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

  private maxTagLength(control: AbstractControl, maxLength: number): ValidationErrors | null {
    if (this.tagList?.errorState != undefined) {
      this.tagList.errorState = false;

      if ((control.value as string).length > maxLength) {
        this.tagList.errorState = true;
        return {maxLength: true};
      }
    }
    return null;
  }

  private backendErrorForTags(control: AbstractControl): ValidationErrors | null {
    if (this.tagList?.errorState != undefined) {
      if (this.errorsProps != null && this.errorsProps['TagList']) {
        this.tagList.errorState = true;
        return {backendErrorForTags: true};
      }
    }
    return null;
  }

  ngOnInit(): void {
    this.backendErrorsPropsSubscription = this.backendErrorsProps
      .pipe(filter((error) => error != null))
      .subscribe((error) => {
        this.errorsProps = error;

        console.log(error);

        this.snackBar.open('Проверьте правильность заполнения полей!', 'X', {
          panelClass: ['snackBar-warning'],
          horizontalPosition: 'end',
          verticalPosition: 'bottom',
          duration: 10000,
        });
      });

    if (this.additionalData) {
      this.realtyTypes = this.additionalData.realtyType;
      this.settlementTypes = this.additionalData.settlementType;
      this.streetTypes = this.additionalData.streetType;
      this.priceTypes = this.additionalData.priceType;
      this.comforts = this.additionalData.comforts;
    }

    if (this.initialValuesProps) {
      this.realtyTypeControl.setValue(this.initialValuesProps.realtyType);

      this.titleControl.setValue(this.initialValuesProps.title);
      this.descriptionControl.setValue(this.initialValuesProps.description);

      this.areaControl.setValue(this.initialValuesProps.area);
      this.numberOfRoomsControl.setValue(this.initialValuesProps.numberOfRooms);
      this.numberOfStoreysControl.setValue(this.initialValuesProps.numberOfStoreys);
      this.storeyControl.setValue(this.initialValuesProps.storey);

      this.regionControl.setValue(this.initialValuesProps.region);
      this.districtControl.setValue(this.initialValuesProps.district);
      this.settlementTypeControl.setValue(this.initialValuesProps.settlementType);
      this.settlementNameControl.setValue(this.initialValuesProps.settlementName);
      this.streetTypeControl.setValue(this.initialValuesProps.streetType);
      this.streetNameControl.setValue(this.initialValuesProps.streetName);
      this.houseNumberControl.setValue(this.initialValuesProps.houseNumber);
      this.apartmentNumberControl.setValue(this.initialValuesProps.apartmentNumber);

      this.priceTypeControl.setValue(this.initialValuesProps.priceType);
      this.priceControl.setValue(this.initialValuesProps.price);

      if (this.initialValuesProps.startOfLease) {
        let startOfLease = moment(this.initialValuesProps.startOfLease);
        let currentDate = moment(this.currentDate);

        if (moment(startOfLease).isSameOrAfter(currentDate)) {
          this.startOfLeaseControl.setValue(moment(this.initialValuesProps.startOfLease).toDate());
        } else {
          this.startOfLeaseControl.setValue(this.currentDate);
        }
      } else {
        this.startOfLeaseControl.setValue(this.currentDate);
      }

      if (this.initialValuesProps.endOfLease) {
        let endOfLease = moment(this.initialValuesProps.endOfLease);
        let minDateEndOfLease = moment(this.minDateEndOfLease(this.currentDate));

        if (moment(endOfLease).isSameOrAfter(minDateEndOfLease)) {
          this.endOfLeaseControl.setValue(moment(this.initialValuesProps.endOfLease).toDate());
        } else {
          this.endOfLeaseControl.setValue(null);
        }
      } else {
        this.endOfLeaseControl.setValue(null);
      }

      this.selectedComfortList = this.initialValuesProps.comfortList.slice();
      this.tags = this.initialValuesProps.tagList.slice();
    }
  }

  minDateEndOfLease(date: Date): Date {
    return date ? moment(date).add(1, 'days').toDate() : moment(this.currentDate).add(1, 'days').toDate();
  }

  addTag(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our tag
    if ((value || '').trim() && this.tags.length < this.tagListLimit) {
      this.tags.push(value.trim());
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  removeTag(tag: TagType): void {
    const index = this.tags.indexOf(tag);

    if (this.tagList?.errorState != undefined) {
      this.tagList.errorState = false;
    }

    if (index >= 0) {
      this.tags.splice(index, 1);
    }
  }

  resetFormAdvert(): void {
    this.initialValuesProps = {
      id: '',
      realtyType: '',
      title: '',
      description: '',
      numberOfRooms: 0,
      area: 0,
      storey: null,
      numberOfStoreys: null,
      region: '',
      district: '',
      settlementType: '',
      settlementName: '',
      streetType: '',
      streetName: '',
      houseNumber: null,
      apartmentNumber: null,
      images: [],
      priceType: null!,
      price: 0,
      startOfLease: null!,
      endOfLease: null,
      createdAd: '',
      updatedAd: null,
      slug: '',
      comfortList: [],
      tagList: [],
      favorited: false,
      author: null!,
    };

    this.ngOnInit();
  }

  onSubmit(): void {
    const advertInput: AdvertInputInterface = {
      ...this.realtyTypeForm.value,
      ...this.descriptionForm.value,
      ...this.apartmentParametersForm.value,
      ...this.addressForm.value,
      ...this.priceForm.value,
      ...this.datepickerForm.value,
      comfortList: this.selectedComfortList.slice(),
      tagList: this.tags.slice(),
    };

    this.advertSubmitEvent.emit(advertInput);
  }

  ngOnDestroy(): void {
    this.backendErrorsPropsSubscription?.unsubscribe;
    this.additionalDataSubscription?.unsubscribe;
  }
}
