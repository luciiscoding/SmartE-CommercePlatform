import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '@app/core/services';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(10),
    ]),
  });
  isLoading: boolean = false;

  constructor(
    private _router: Router,
    private _userService: UserService,
    private _toastrService: ToastrService
  ) {}

  loginUser(): void {
    this.isLoading = true;
    this._userService
      .loginUser({
        email: this.loginForm.value.email,
        password: this.loginForm.value.password,
      })
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe({
        next: () => {
          this._router.navigate(['/home']);
          this._userService.userLoggedIn$.next(true);
          this._toastrService.success('Login successful');
        },
        error: () => {
          this._toastrService.error('Invalid username or password');
        },
      });
  }

  redirectToRegister(): void {
    this._router.navigate(['/register']);
  }
}
