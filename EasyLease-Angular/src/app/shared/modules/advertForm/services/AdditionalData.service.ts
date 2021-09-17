import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {environment} from 'src/environments/environment';
import {AdvertAdditionalData} from '../types/advertAdditionalData.interface';

@Injectable()
export class AdditionalDataService {
  constructor(private http: HttpClient) {}

  getAdditionalDataForAdvert(): Observable<AdvertAdditionalData> {
    const fullUrl = `${environment.apiUrl}/adverts/additional-data`;

    return this.http.get<AdvertAdditionalData>(fullUrl).pipe(map((response: AdvertAdditionalData) => response));
  }
}
