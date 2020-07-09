import { Component, OnInit } from '@angular/core';
import { LoginRequest } from '../dto/loginRequest';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from './../user.service';
import { StorageService } from './../storage.service';

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

  login(form): void {
    const loginRequest = this.getLoginRequest(form);
    this.userService.login(loginRequest).subscribe((res) => {
      this.storageService.storeToken(res);
      console.log(this.storageService.getUserInfos());
      this.router.navigate(['main']);
    });
  }
  getLoginRequest(form: FormGroup): LoginRequest {
    const loginRequest = {
      email: form.controls.email.value,
      password: form.controls.password.value,
    };
    return loginRequest;
  }
  ngOnInit(): void {}
}
