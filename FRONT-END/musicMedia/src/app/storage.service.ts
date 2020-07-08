import { Injectable } from '@angular/core';
import * as jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  constructor() {}
  private key = 'Token';
  storeToken(token: string): void {
    localStorage.setItem(this.key, token['access_Token']);
  }
  getToken(): string {
    return localStorage.getItem(this.key);
  }
  getUserInfos(): any {
    try {
      const {user} = jwt_decode(this.getToken())
      return user;
    } catch (Error) {
      return null;
    }
  }
}
