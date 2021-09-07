import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ProfileInterface} from 'src/app/shared/types/profile.Interface';
import {environment} from 'src/environments/environment';
import {GetUserProfileInterface} from '../types/getUserProfile.interface';

@Injectable()
export class UserProfileService {
  constructor(private http: HttpClient) {}

  getUserProfile(slug: string): Observable<ProfileInterface> {
    const url = `${environment.apiUrl}/profile/${slug}`;

    return this.http
      .get<GetUserProfileInterface>(url)
      .pipe(map((response: GetUserProfileInterface) => response.profile));
  }
}
