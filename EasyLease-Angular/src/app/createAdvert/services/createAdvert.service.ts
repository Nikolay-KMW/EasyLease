import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {AdvertInputInterface} from 'src/app/shared/types/advertInput.interface';
import {SaveAdvertResponseInterface} from 'src/app/shared/types/saveAdvertResponse.interface';
import {environment} from 'src/environments/environment';

@Injectable()
export class CreateAdvertService {
  constructor(private http: HttpClient) {}

  createAdvert(advertInput: AdvertInputInterface): Observable<AdvertInterface> {
    const fullUrl = environment.apiUrl + '/articles';

    return this.http
      .post<SaveAdvertResponseInterface>(fullUrl, advertInput)
      .pipe(map((response: SaveAdvertResponseInterface) => response.article));
  }
}
