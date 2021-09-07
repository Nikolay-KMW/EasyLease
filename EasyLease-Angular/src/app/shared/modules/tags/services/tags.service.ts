import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

import {TagType} from 'src/app/shared/types/tag.type';
import {environment} from 'src/environments/environment';
import {GetTagsResponseInterface} from '../types/getTagsResponse.interface';

@Injectable()
export class TagsService {
  constructor(private http: HttpClient) {}

  getTags(url: string): Observable<TagType[]> {
    const fullUrl = environment.apiUrl + url;

    return this.http.get<GetTagsResponseInterface>(fullUrl).pipe(
      map((response: GetTagsResponseInterface) => {
        return response.tags;
      })
    );
  }
}
