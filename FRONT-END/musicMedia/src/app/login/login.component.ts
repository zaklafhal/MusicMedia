import { Component, OnInit } from '@angular/core';
import { LoginRequest } from '../dto/loginRequest';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from './../services/user.service';
import { StorageService } from './../services/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  public form = this.formBuilder.group({
    email: ['', [Validators.email, Validators.required]],
    password: ['', Validators.required],
  });
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userService: UserService,
    private storageService: StorageService
  ) {}
  errorRequired: string = 'This field is required';
  errorEmail: string =
    this.form.controls.email.value === ''
      ? this.errorRequired
      : 'Invalid email address';
  error: string;
  login(form: FormGroup): void {
    const loginRequest = this.getLoginRequest(form);
    this.userService.login(loginRequest).subscribe(
      (res) => {
        this.storageService.storeToken(res);
        location.assign('/main');
      },
      (e) => (this.error = e.error)
    );
  }
  getLoginRequest(form: FormGroup): LoginRequest {
    const { controls } = form;
    const loginRequest = {
      email: controls.email.value,
      password: controls.password.value,
    };
    return loginRequest;
  }
  ngOnInit(): void {}
}
