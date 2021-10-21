import {BreakpointObserver, Breakpoints, BreakpointState} from '@angular/cdk/layout';
import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {IconDefinition} from '@fortawesome/fontawesome-svg-core';
import {faSave, faSpinner, faTimesCircle} from '@fortawesome/free-solid-svg-icons';
import {select, Store} from '@ngrx/store';
import {Observable} from 'rxjs';
import {map, shareReplay} from 'rxjs/operators';
import {setDescriptionAction, setTitleAction} from 'src/app/shared/modules/banner/store/action/sync.action';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {environment} from 'src/environments/environment';
import {deleteAllPhotoAction} from '../../store/actions/deleteAllPhoto.action';
import {getPhotoForAdvertAction} from '../../store/actions/getPhoto.action';
import {uploadPhotoAction} from '../../store/actions/uploadPhoto.action';
import {
  isDeletingSelector,
  isFallingSelector,
  isLoadingSelector,
  isSubmittingSelector,
  photosSelector,
  validationErrorsSelector,
} from '../../store/selectors';
import {TypeOperation} from '../../types/typeOperation.enum';

@Component({
  selector: 'el-upload-advert-photo',
  templateUrl: './uploadAdvertPhoto.component.html',
  styleUrls: ['./uploadAdvertPhoto.component.scss'],
})
export class UploadAdvertPhotoComponent implements OnInit {
  photos: File[] | null = null;
  initialValues$: Observable<File[] | null>;

  isHandset$: Observable<boolean>;

  isLoading$: Observable<boolean>;
  isSubmitting$: Observable<boolean>;
  isDeleting$: Observable<boolean>;
  isFalling$: Observable<boolean>;
  backendErrors$: Observable<BackendErrorInterface | null>;
  slug: string | null;
  type: string | null;

  TypeOperation = TypeOperation;

  maxFileSize: number = environment.fileSizeLimit;
  maxNumberPhoto: number = environment.numberOfFilesLimit;
  allowedExtensions: string[] = environment.allowedExtensions;

  faSpinner: IconDefinition = faSpinner;
  faTimesCircle: IconDefinition = faTimesCircle;
  faSave: IconDefinition = faSave;

  constructor(
    private store: Store<AppStateInterface>,
    private route: ActivatedRoute,
    private router: Router,
    private breakpointObserver: BreakpointObserver
  ) {
    // Initialize values
    this.isHandset$ = this.breakpointObserver.observe([Breakpoints.Handset, Breakpoints.TabletPortrait]).pipe(
      map((result: BreakpointState) => result.matches),
      shareReplay()
    );

    this.slug = route.snapshot.paramMap.get('slug');
    this.type = route.snapshot.paramMap.get('mode');

    this.isLoading$ = this.store.pipe(select(isLoadingSelector));
    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector));
    this.isDeleting$ = this.store.pipe(select(isDeletingSelector));
    this.isFalling$ = this.store.pipe(select(isFallingSelector));
    this.backendErrors$ = this.store.pipe(select(validationErrorsSelector));
    this.initialValues$ = this.store.pipe(select(photosSelector));

    // Fetch data
    if (this.slug && this.type === TypeOperation.Editing) {
      this.store.dispatch(getPhotoForAdvertAction({slug: this.slug}));
    }
  }

  ngOnInit(): void {
    this.setValueBannerModule();
  }

  setValueBannerModule(): void {
    this.store.dispatch(setTitleAction({title: 'Редактор фото'}));
    this.store.dispatch(setDescriptionAction({description: 'Вы можете здесь добавлять и удалять фото'}));
  }

  putPhoto(files: File[]): void {
    this.photos = files;
  }

  onSubmit(): void {
    if (this.slug && this.photos && this.photos.length > 0) {
      this.store.dispatch(uploadPhotoAction({slug: this.slug, files: this.photos}));
    } else if (this.slug) {
      this.router.navigate(['/advert', this.slug]);
    } else {
      this.router.navigateByUrl('/');
    }
  }

  onDeleteAllPhoto(): void {
    if (this.slug) {
      this.store.dispatch(deleteAllPhotoAction({slug: this.slug}));
    }
  }
}
