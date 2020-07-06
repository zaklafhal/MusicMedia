import { Component, OnInit } from '@angular/core';
import { LoginRequest } from '../dto/loginRequest';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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
  public form = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
  });
  constructor(private formBuilder: FormBuilder) {}
  signIn(form): void {
    console.log(form.controls.email.value);
  }
  ngOnInit(): void {}
}
