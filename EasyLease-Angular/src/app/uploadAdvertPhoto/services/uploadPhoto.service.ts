import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {SaveAdvertResponseInterface} from 'src/app/shared/types/saveAdvertResponse.interface';
import {environment} from 'src/environments/environment';

@Injectable()
export class UploadPhotoService {
  constructor(private http: HttpClient) {}

  private toFormData(key: string, files: File[]) {
    const formData = new FormData();

    for (const file of files) {
      formData.append(key, file);
    }
    return formData;
  }

  uploadPhotoForAdvert(slag: string, files: File[]): Observable<AdvertInterface> {
    const fullUrl = environment.apiUrl + `/realty/${slag}/photos`;

    return this.http
      .put<SaveAdvertResponseInterface>(fullUrl, this.toFormData('photos', files))
      .pipe(map((response: SaveAdvertResponseInterface) => response.advert));
  }
}
