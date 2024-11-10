import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '@app/core';
import { filter } from 'rxjs';

@Component({
  selector: 'smart-ecommerce-platform-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  username: string = '';

  constructor(userService: UserService, private _router: Router) {
    userService.onUserLogin$.pipe(filter((val) => val)).subscribe(() => {
      console.log('User logged in');
      userService
        .getUserById(sessionStorage.getItem('userId')!)
        .subscribe((user) => {
          this.username = user.username;
        });
    });
  }

  onLogoutClick(): void {
    this.username = '';
    sessionStorage.clear();
    this._router.navigate(['/login']);
  }
}
