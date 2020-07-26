import { Injectable } from '@angular/core';
import * as jwt_decode from 'jwt-decode';
import { UserInfos } from './../dto/userInfos';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  constructor() {}
  private key: string = 'Token';
  public user: UserInfos = this.getUserInfos();
  storeToken(token: string): void {
    localStorage.setItem(this.key, token['access_Token']);
  }
  getToken(): string {
    return localStorage.getItem(this.key);
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
    localStorage.removeItem(this.key);
  }
}
