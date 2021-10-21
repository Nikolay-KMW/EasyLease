import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {SaveAdvertResponseInterface} from 'src/app/shared/types/saveAdvertResponse.interface';
import {environment} from 'src/environments/environment';

@Injectable()
export class DeleteAllPhotoService {
  constructor(private http: HttpClient) {}

  deleteAllPhotoForAdvert(slag: string): Observable<AdvertInterface> {
    const fullUrl = environment.apiUrl + `/realty/${slag}/photos`;

    return this.http
      .delete<SaveAdvertResponseInterface>(fullUrl)
      .pipe(map((response: SaveAdvertResponseInterface) => response.advert));
  }
}
