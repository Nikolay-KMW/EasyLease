import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map, timeout} from 'rxjs/operators';

import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {SaveAdvertResponseInterface} from 'src/app/shared/types/saveAdvertResponse.interface';
import {environment} from 'src/environments/environment';

@Injectable()
export class DownloadPhotoService {
  typeFiles: string;

  constructor(private http: HttpClient) {
    this.typeFiles = environment.allowedExtensions
      .map((ext) => ext.substring(1))
      .map((ext) => (ext = `image/${ext}`))
      .toString();
  }

  // private toFormData(key: string, files: File[]) {
  //   const formData = new FormData();

  //   for (const file of files) {
  //     formData.append(key, file);
  //   }
  //   return formData;
  // }

  // downloadFile(id): Observable<Blob> {
  //   let options = new RequestOptions({responseType: ResponseContentType.Blob});
  //   return this.http
  //     .get(this._baseUrl + '/' + id, options)
  //     .map((res) => res.blob())
  //     .catch(this.handleError);
  // }

  downloadPhotoForAdvert(imagePath: string): Observable<File> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/image',
      }),
      responseType: 'blob',
    };

    const headers = new HttpHeaders()
      // .set('Access-Control-Allow-Headers', 'Content-Type')
      // .set('Access-Control-Allow-Methods', 'GET')
      .set('Access-Control-Allow-Origin', 'https://localhost:4200')
      .set('Content-Type', 'image/jpeg')
      .set('Accept', 'image/*');

    // headers.append('Access-Control-Allow-Headers', 'Content-Type');
    // headers.append('Access-Control-Allow-Methods', 'GET');
    // headers.append('Access-Control-Allow-Origin', 'https://localhost:5001');
    // headers.append('Content-Type', 'image/jpeg');
    // headers.append('Accept', 'image/jpeg');

    const headers1 = new HttpHeaders({'sec-fetch-mode': 'no-cors'});

    const downloadUrl = environment.uploadUrl + imagePath;

    // this.http
    //   .get<any>(downloadUrl)
    //   .pipe()
    //   .subscribe((res: any) => {
    //     let blob: any = new Blob([res], {type: 'image/jpeg'});
    //     console.log('res---', blob);
    //   });

    return this.http.get<Blob>(downloadUrl, {headers: headers, responseType: 'blob' as 'json'}).pipe(
      map((photo) => {
        console.log('*******http*******', photo);
        return photo as File;
      })
    );

    // return this.http.get(downloadUrl, {responseType: 'arraybuffer', headers: headers}).pipe(
    //   map((photo) => {
    //     console.log('******http********', photo);
    //     let blob = new Blob([photo], {type: 'image/jpeg'});
    //     return blob as File;
    //   })
    // );

    // return this.http.get<File>(downloadUrl, {headers: headers}).pipe(
    //   timeout(50000),
    //   map((photo) => {
    //     console.log('ssadawdwsdawd', photo);
    //     return photo;
    //   })
    // );

    // for (const imagePath of imagesPath) {
    //   const downloadUrl = environment.uploadUrl + imagesPath;

    //   return this.http.get<File>(downloadUrl);
    // }
  }
}
