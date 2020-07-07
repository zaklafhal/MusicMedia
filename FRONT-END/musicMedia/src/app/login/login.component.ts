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
    email: ['', [Validators.email, Validators.required]],
    password: ['', Validators.required],
  });
  constructor(private formBuilder: FormBuilder) {}
  errorRequired: string = "This field is required" ;
  errorEmail: string =  this.form.controls.email.value === "" ?  this.errorRequired : "Invalid email address";
  signIn(form): void {
    console.log("Login");
  }
  ngOnInit(): void {}
}
