import { Injectable } from '@angular/core';
import { Observable, throwError, from } from 'rxjs';
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import config from './config.json';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class SpotifyService {
  constructor(private http: HttpClient) {}
  public token;
  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // client or network error
    } else {
      // Backend error
    }

    return throwError('erro');
  }

  setToken(): Observable<any> {
    const headers = new HttpHeaders({
      Authorization:
        'Basic  ' + btoa(config.client_id + ':' + config.client_secret),
      'Content-Type': 'application/x-www-form-urlencoded;',
    });
    const body = 'grant_type=client_credentials';
    return this.http
      .post<any>('https://accounts.spotify.com/api/token', body, {
        headers: headers,
      })
      .pipe(
        catchError(this.handleError),
        map((res) => res['access_token'])
      );
  }
  getToken() {
    this.setToken().subscribe((res) => (this.token = res));
  }

  searchArtist(artistName: string): Observable<any> {
    const url = `https://api.spotify.com/v1/search?query=${artistName}&offset=0&limit=20&type=artist`;
    const headers = new HttpHeaders({
      Authorization: 'Bearer  ' + this.token,
    });
    return this.http
      .get<any>(url, { headers: headers })
      .pipe(
        catchError(this.handleError),
        map((res) => console.log(res))
      );
  }
}
