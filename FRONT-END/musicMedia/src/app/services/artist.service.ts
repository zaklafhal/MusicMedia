import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginRequest } from './../dto/loginRequest';
import { environment } from './../../environments/environment';
import { Artist } from '../model/artist';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root',
})
export class ArtistService {
  constructor(private http: HttpClient, private storage: StorageService) {}
  private endpoint = `${environment.endpoint}artists`;

  private artists: Artist[];

  setArtists(artists: Artist[]): void {
    this.artists = artists;
  }

  getArtists(): Artist[] {
    return this.artists;
  }

  containsArtist(artist: Artist): boolean {
    if (!this.artists) return false;
    return this.artists.includes(artist);
  }

  addArtist(artist: Artist): Observable<any> {
    console.log(this.storage.user);
    if (this.storage.user) {
      console.log(this.storage.getToken());
      const headers = new HttpHeaders({
        Authorization: 'Bearer  ' + this.storage.getToken(),
        'Content-Type': 'application/json',
      });
      return this.http.post<any>(this.endpoint, artist, { headers: headers });
    }
  }
}
