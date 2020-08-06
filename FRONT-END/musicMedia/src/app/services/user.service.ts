import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  HttpClient,
  HttpErrorResponse,
} from '@angular/common/http';
import { LoginRequest } from './../dto/loginRequest';
import { environment } from './../../environments/environment';
import { RegisterRequest } from '../dto/registerRequest';


@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}
  public handleError(error: HttpErrorResponse): string {
    console.log(error);
    const { errors } = error.error;
    if (error.error instanceof ErrorEvent) {
      // client or network error
    } else {
      let errorMsg = '';
      console.log(errors);
      const keys = Object.keys(errors);
      console.log(keys);
      keys.forEach((key) => {
        if (errors[key]) {
          errorMsg += errors[key][0] + '\n';
        }
      });
      if (errorMsg) {
        return errorMsg;
      }
    }
  }
  private loginUrl = environment.endpoint + 'token';
  private registerUrl = environment.endpoint + 'user';
  login(loginRequest: LoginRequest): Observable<string> {
    return this.http.post<string>(this.loginUrl, loginRequest);
  }
  register(registerRequest: RegisterRequest): Observable<any> {
    return this.http.post<any>(this.registerUrl, registerRequest);
  }
}
