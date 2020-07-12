import { Component } from '@angular/core';
import { StorageService } from './storage.service';
import { UserInfos } from './dto/userInfos';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Music Media';

  constructor(private storageService: StorageService) {}
  user: UserInfos = this.storageService.user;
  logout(): void {
    this.storageService.logout();
    location.assign('/main');
  }
}
