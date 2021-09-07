import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {environment} from 'src/environments/environment';
import {AdvertInterface} from '../types/advert.interface';
import {GetAdvertResponseInterface} from '../types/getAdvertResponse.interface';

@Injectable()
export class AdvertService {
  constructor(private http: HttpClient) {}
  getAdvert(slug: string): Observable<AdvertInterface> {
    const fullUrl = `${environment.apiUrl}/adverts/${slug}`;

    return this.http
      .get<GetAdvertResponseInterface>(fullUrl)
      .pipe(map((response: GetAdvertResponseInterface) => response.advert));
  }
}
