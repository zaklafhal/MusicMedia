import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { RegisterRequest } from '../dto/registerRequest';
import { UserService } from '../services/user.service';
import { StorageService } from '../services/storage.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  public form = this.formBuilder.group({
    email: ['', [Validators.email, Validators.required]],
    name: ['', Validators.required],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
  });
  error: string;
  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private storageService: StorageService
  ) {}
  register(form: FormGroup): void {
    const registerRequest = this.getRegisterRequest(form);
    console.log(registerRequest);
    this.userService.register(registerRequest).subscribe((res) => {
      const { email, password } = registerRequest;
      const loginRequest = {
        email: email,
        password: password,
      };
      this.userService.login(loginRequest).subscribe((res) => {
        this.storageService.storeToken(res);
        location.assign('/main');
      });
    });
  }
  getRegisterRequest(form: FormGroup): RegisterRequest {
    const { controls } = form;
    const registerRequest = {
      email: controls.email.value,
      name: controls.name.value,
      password: controls.password.value,
      confirmPassword: controls.confirmPassword.value,
    };
    return registerRequest;
  }
  validatePasswords(form: FormGroup) { 
  const password = form.get('password').value;
  const confirmPassword = form.get('confirmPassword').value;

  return password === confirmPassword ? null : { notSame: true }     
}

  ngOnInit(): void {}
}
