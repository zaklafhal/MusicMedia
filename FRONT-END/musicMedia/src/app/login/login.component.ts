import { Component, OnInit } from '@angular/core';
import { LoginRequest } from '../dto/loginRequest';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from './../user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginRequest: LoginRequest = {
    email: 'test@test.ca',
    password: 'Passw0rd!',
  };
  public form = this.formBuilder.group({
    email: ['', [Validators.email, Validators.required]],
    password: ['', Validators.required],
  });
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService
  ) {}
  errorRequired: string = 'This field is required';
  errorEmail: string =
    this.form.controls.email.value === ''
      ? this.errorRequired
      : 'Invalid email address';
  signIn(form): void {
    this.userService
      .login(this.loginRequest)
      .subscribe((res) => console.log(res));
    this.router.navigate(['main']);
  }
  ngOnInit(): void {}
}
