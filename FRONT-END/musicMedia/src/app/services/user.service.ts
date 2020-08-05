import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginRequest } from './../dto/loginRequest';
import { environment } from './../../environments/environment';
import { RegisterRequest } from '../dto/registerRequest';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {
    
  }
  private loginUrl = environment.endpoint + 'token';
  private registerUrl = environment.endpoint + 'user'
  login(loginRequest :LoginRequest) : Observable<string>{
    return this.http.post<string>(this.loginUrl,loginRequest);
  }
  register(registerRequest: RegisterRequest) : Observable<any>{
    return this.http.post<any>(this.registerUrl,registerRequest);
  }
}
