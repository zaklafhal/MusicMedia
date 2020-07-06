import { Component, OnInit } from '@angular/core';
import { LoginRequest } from '../dto/loginRequest';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginRequest: LoginRequest = {
    email: 'myemail@gmail.com',
    password: 'Passw0rd!',
  };
  constructor() {}

  ngOnInit(): void {}
}
