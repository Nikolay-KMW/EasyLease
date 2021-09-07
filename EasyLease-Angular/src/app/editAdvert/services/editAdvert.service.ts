import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {AdvertInputInterface} from 'src/app/shared/types/advertInput.interface';
import {SaveAdvertResponseInterface} from 'src/app/shared/types/saveAdvertResponse.interface';
import {environment} from 'src/environments/environment';

@Injectable()
export class EditAdvertService {
  constructor(private http: HttpClient) {}

  updateAdvert(slug: string, advertInput: AdvertInputInterface): Observable<AdvertInterface> {
    const fullUrl = `${environment.apiUrl}/adverts/${slug}`;

    return this.http
      .put<SaveAdvertResponseInterface>(fullUrl, advertInput)
      .pipe(map((response: SaveAdvertResponseInterface) => response.advert));
  }
}
