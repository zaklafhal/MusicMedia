import { Injectable } from '@angular/core';
import * as jwt_decode from 'jwt-decode';
import { UserInfos } from './../dto/userInfos';
import { Artist } from '../model/artist';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  constructor() {}
  private tokenKey: string = 'Token';
  private artistsKey: string = 'Artists';
  public user: UserInfos = this.getUserInfos();
  storeToken(token: string): void {
    localStorage.setItem(this.tokenKey, token['access_Token']);
  }
  getToken(): string {
    return localStorage.getItem(this.tokenKey);
  }
  storeArtists(artists: Artist[]): void {
    const data = JSON.stringify(artists);
    localStorage.setItem(this.artistsKey, data);
  }
  getArtists(): Artist[] {
    const data = localStorage.getItem(this.artistsKey);
    const artists = JSON.parse(data);
    return artists;
  }
  getUserInfos(): UserInfos {
    try {
      const { user } = jwt_decode(this.getToken());
      return user;
    } catch (Error) {
      return null;
    }
  }
  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.artistsKey);
  }
}
