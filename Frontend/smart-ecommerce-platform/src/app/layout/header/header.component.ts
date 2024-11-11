import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '@app/core';
import { Product } from '@app/features';
import { CartService } from '@app/features/services';
import { ToastrService } from 'ngx-toastr';
import { filter } from 'rxjs';

@Component({
  selector: 'smart-ecommerce-platform-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  username: string = '';
  isCartOpen = false;
  cartItems: Product[] = [];

  constructor(
    userService: UserService,
    private _cartService: CartService,
    private _router: Router,
    private _toastrService: ToastrService
  ) {
    if (localStorage.getItem('token') !== null) {
      userService.getUserDetails().subscribe((user) => {
        this.username = user.username!;
      });
      _cartService.cartUpdated$.subscribe(() => {
        _cartService.getCartItems().subscribe((cart) => {
          this.cartItems = cart.products;
        });
      });
    }
  }

  toggleCart(): void {
    this.isCartOpen = !this.isCartOpen;
  }

  checkout(): void {
    this.isCartOpen = false;
  }

  removeItem(index: number): void {
    this._cartService.removeFromCart(this.cartItems[index].id!).subscribe({
      next: () => {
        this._toastrService.success('Item removed from cart');
        this._cartService.cartUpdated$.next();
      },
      error: () => {},
    });
  }

  onLogoutClick(): void {
    this.username = '';
    localStorage.clear();
    this._router.navigate(['/login']);
  }
}
