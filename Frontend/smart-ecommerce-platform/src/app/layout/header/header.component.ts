import { Component, HostListener } from '@angular/core';
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
  isCartOpen = false;
  cartItems = [
    { name: 'Item 1', price: 10.99 },
    { name: 'Item 2', price: 25.5 },
    { name: 'Item 3', price: 7.25 },
  ];

  constructor(userService: UserService, private _router: Router) {
    userService.onUserLogin$.pipe(filter((val) => val)).subscribe(() => {
      userService.getUserDetails().subscribe((user) => {
        this.username = user.username!;
      });
    });
  }

  toggleCart(): void {
    this.isCartOpen = !this.isCartOpen;
  }

  checkout(): void {
    this.isCartOpen = false;
  }

  removeItem(index: number): void {
    this.cartItems.splice(index, 1);
  }

  onLogoutClick(): void {
    this.username = '';
    localStorage.clear();
    this._router.navigate(['/login']);
  }
}
