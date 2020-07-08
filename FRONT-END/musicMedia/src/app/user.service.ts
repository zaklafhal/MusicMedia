import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginRequest } from './dto/loginRequest';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {
    
  }
  private url = 'https://localhost:44314/';
  private loginUrl = this.url + 'api/token';
  login(loginRequest :LoginRequest) : Observable<string>{
    return this.http.post<string>(this.loginUrl,loginRequest);
  }
}
