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
  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // client or network error
    } else {
      // Backend error
    }

    return throwError('erro');
  }
  getAuth(): Observable<any> {
    const headers = new HttpHeaders();
    headers.append(
      'Authorization',
      'Basic ' + btoa(config.client_id + ':' + config.client_secret)
    );
    headers.append('Content-Type', 'application/x-www-form-urlencoded');

    const params: URLSearchParams = new URLSearchParams();
    params.set('grant_type', 'client_credentials');
    const body = params.toString();

    return this.http
      .post<any>('https://accounts.spotify.com/api/token', body, {
        headers: headers,
      })
      .pipe(
        catchError(this.handleError),
        map((res) => res.json)
      );
  }

  searchArtist(artistName: string): Observable<any> {
    const url = `https://api.spotify.com/v1/search?query=${artistName}&offset=0&limit=20&type=artist`;
    return this.http.get<any>(url).pipe(
      catchError(this.handleError),
      map((res) => res.json)
    );
  }
}
