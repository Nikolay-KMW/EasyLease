import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {AdvertInterface} from 'src/app/shared/types/advert.interface';
import {GetAdvertResponseInterface} from 'src/app/shared/types/getAdvertResponse.interface';
import {environment} from 'src/environments/environment';

@Injectable()
export class AddToFavoritesService {
  constructor(private http: HttpClient) {}

  getUrl(slug: string): string {
    return `${environment.apiUrl}/realty/${slug}/favorite`;
  }

  getAdvert(response: GetAdvertResponseInterface): AdvertInterface {
    return response.advert;
  }

  addToFavorites(slug: string): Observable<AdvertInterface> {
    const url = this.getUrl(slug);
    return this.http.post<GetAdvertResponseInterface>(url, {}).pipe(map(this.getAdvert));
  }

  removeFromFavorites(slug: string): Observable<AdvertInterface> {
    const url = this.getUrl(slug);
    return this.http.delete<GetAdvertResponseInterface>(url).pipe(map(this.getAdvert));
  }
}
