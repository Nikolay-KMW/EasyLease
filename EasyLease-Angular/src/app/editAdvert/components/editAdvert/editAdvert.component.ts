import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {select, Store} from '@ngrx/store';
import {Observable} from 'rxjs';
import {setDescriptionAction, setTitleAction} from 'src/app/shared/modules/banner/store/action/sync.action';

import {AdvertInputInterface} from 'src/app/shared/types/advertInput.interface';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {getAdvertAction} from '../../store/actions/getAdvert.action';
import {updateAdvertAction} from '../../store/actions/updateAdvert.action';
import {advertSelector, isLoadingSelector, isSubmittingSelector, validationErrorsSelector} from '../../store/selectors';

@Component({
  selector: 'el-edit-advert',
  templateUrl: './editAdvert.component.html',
  styleUrls: ['./editAdvert.component.scss'],
})
export class EditAdvertComponent implements OnInit {
  initialValues$!: Observable<AdvertInputInterface | null>;
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
    this.initialValues$ = this.store.pipe(select(advertSelector));

    // Fetch data
    if (this.slug) {
      this.store.dispatch(getAdvertAction({slug: this.slug}));
    }
  }

  ngOnInit(): void {
    this.setValueBannerModule();
  }

  setValueBannerModule(): void {
    this.store.dispatch(setTitleAction({title: 'Редактор объявления'}));
    this.store.dispatch(setDescriptionAction({description: 'Вы можете здесь редактировать свое объявление'}));
  }

  onSubmit(advertInput: AdvertInputInterface): void {
    //console.log('onSubmit in parent', advertInput);
    if (this.slug) {
      this.store.dispatch(updateAdvertAction({slug: this.slug, advertInput}));
    }
  }
}
