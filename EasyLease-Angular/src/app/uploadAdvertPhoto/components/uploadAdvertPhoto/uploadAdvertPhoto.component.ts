import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {select, Store} from '@ngrx/store';
import {Observable} from 'rxjs';

import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {getPhotoForAdvertAction} from '../../store/actions/getPhoto.action';
import {isLoadingSelector, isSubmittingSelector, photosSelector, validationErrorsSelector} from '../../store/selectors';

@Component({
  selector: 'el-upload-advert-photo',
  templateUrl: './uploadAdvertPhoto.component.html',
  styleUrls: ['./uploadAdvertPhoto.component.scss'],
})
export class UploadAdvertPhotoComponent implements OnInit {
  //initialValues: File[] | null = null;
  initialValues$: Observable<File[] | null>;

  isLoading$: Observable<boolean>;
  isSubmitting$: Observable<boolean>;
  backendErrors$: Observable<BackendErrorInterface | null>;
  slug: string | null;

  constructor(private store: Store<AppStateInterface>, private route: ActivatedRoute) {
    // Initialize values
    this.slug = route.snapshot.paramMap.get('slug');

    this.isLoading$ = this.store.pipe(select(isLoadingSelector));
    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector));
    this.backendErrors$ = this.store.pipe(select(validationErrorsSelector));
    this.initialValues$ = this.store.pipe(select(photosSelector));

    // Fetch data
    if (this.slug) {
      this.store.dispatch(getPhotoForAdvertAction({slug: this.slug}));
    }
  }

  ngOnInit(): void {}
}
