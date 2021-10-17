import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ImageInterface} from 'src/app/shared/types/image.interface';

import {environment} from 'src/environments/environment';

@Injectable()
export class DownloadPhotoService {
  constructor(private http: HttpClient) {}

  downloadPhotoForAdvert(image: ImageInterface): Observable<File> {
    const downloadUrl = environment.uploadUrl + image.path;

    const headers = new HttpHeaders()
      // .set('Access-Control-Allow-Headers', 'Content-Type')
      // .set('Access-Control-Allow-Methods', 'GET')
      .set('Access-Control-Allow-Origin', environment.originUrl)
      .set('Content-Type', 'image/jpeg')
      .set('Accept', 'image/*');

    return this.http.get(downloadUrl, {responseType: 'arraybuffer', headers: headers}).pipe(
      map((photo) => {
        let blob = new File([photo], image.name, {type: 'image/jpeg'});
        return blob as File;
      })
    );

    // return this.http.get<Blob>(downloadUrl, {headers: headers, responseType: 'blob' as 'json'}).pipe(
    //   map((photo) => {
    //     let file = new File([photo], image.name, {type: 'image/jpeg'});
    //     return file;
    //   })
    // );
  }
}
