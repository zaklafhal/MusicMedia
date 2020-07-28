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

  containsArtist(artist: Artist): boolean {
    const artists = this.storage.getArtists();
    if (!artists) return false;
    const containsArtist = artists.some((a) => a.spotifyId == artist.spotifyId);
    return containsArtist;
  }

  addArtist(artist: Artist): Observable<any> {
    if (this.storage.user) { 
      const headers = new HttpHeaders({
        Authorization: 'Bearer  ' + this.storage.getToken(),
        'Content-Type': 'application/json',
      });
      return this.http.post<any>(this.endpoint, artist, { headers: headers });
    }
  }
}
