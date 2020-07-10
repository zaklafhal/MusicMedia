import { Component } from '@angular/core';
import { StorageService } from './storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Music Media';

  constructor(private storageService: StorageService) {}
  
  logout(): void {
    this.storageService.logout();
  }
}
