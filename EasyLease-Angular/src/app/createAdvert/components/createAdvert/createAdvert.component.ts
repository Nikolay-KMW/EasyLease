import {Component, OnInit} from '@angular/core';
import {select, Store} from '@ngrx/store';
import {Observable} from 'rxjs';
import {setDescriptionAction, setTitleAction} from 'src/app/shared/modules/banner/store/action/sync.action';
import {AdvertInterface} from 'src/app/shared/types/advert.interface';

import {AdvertInputInterface} from 'src/app/shared/types/advertInput.interface';
import {AppStateInterface} from 'src/app/shared/types/appState.interface';
import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {createAdvertAction} from '../../store/actions/createAdvert.action';
import {isSubmittingSelector, validationErrorsSelector} from '../../store/selectors';

@Component({
  selector: 'el-create-advert',
  templateUrl: './createAdvert.component.html',
  styleUrls: ['./createAdvert.component.scss'],
})
export class CreateAdvertComponent implements OnInit {
  initialValues: AdvertInterface | null = null;

  isSubmitting$: Observable<boolean>;
  backendErrors$: Observable<BackendErrorInterface | null>;

  constructor(private store: Store<AppStateInterface>) {
    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector));
    this.backendErrors$ = this.store.pipe(select(validationErrorsSelector));
  }

  ngOnInit(): void {
    this.setValueBannerModule();
  }

  setValueBannerModule(): void {
    this.store.dispatch(setTitleAction({title: 'Создание объявления'}));
    this.store.dispatch(
      setDescriptionAction({
        description: 'Здесь Ви можете создать нужное Вам объявление',
      })
    );
  }

  onSubmit(advertInput: AdvertInputInterface): void {
    //console.log('onSubmit in parent', advertInput);

    this.store.dispatch(createAdvertAction({advertInput}));
  }
}
