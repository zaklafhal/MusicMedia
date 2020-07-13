import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginRequest } from './dto/loginRequest';
import { environment } from './../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {
    
  }
  private loginUrl = environment.endpoint + 'token';
  login(loginRequest :LoginRequest) : Observable<string>{
    return this.http.post<string>(this.loginUrl,loginRequest);
  }
}
