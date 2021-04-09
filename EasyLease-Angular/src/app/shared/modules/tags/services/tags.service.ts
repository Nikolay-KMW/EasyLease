import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';

import {environment} from 'src/environments/environment';
import {GetTagsResponseInterface} from '../types/getTagsResponse.interface';

@Injectable()
export class TagsService {
  constructor(private http: HttpClient) {}
  getTags(url: string): Observable<GetTagsResponseInterface> {
    const fullUrl = environment.apiUrl + url;

    return this.http.get<GetTagsResponseInterface>(fullUrl);
  }
}
