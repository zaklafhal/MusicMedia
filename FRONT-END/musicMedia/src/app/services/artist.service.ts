import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginRequest } from './../dto/loginRequest';
import { environment } from './../../environments/environment';
import { Artist } from '../model/artist';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class ArtistService {

  constructor(private http : HttpClient, private storage: StorageService) { }
  private endpoint = `${environment.endpoint} + artists` 

  addArtist(artist: Artist): void{
    if(this.storage.user){
      const headers = new HttpHeaders({
        Authorization:
          'Bearer  ' + this.storage.getToken(),
        'Content-Type': 'application/x-www-form-urlencoded;',
      });
      this.http.post(this.endpoint,artist, {headers: headers})
    }
  }
}
