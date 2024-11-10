import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '@app/core/services';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  registerForm: FormGroup = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(10),
    ]),
    email: new FormControl('', [Validators.required, Validators.email]),
  });
  isLoading: boolean = false;
  constructor(
    private _router: Router,
    private _userService: UserService,
    private _toastrService: ToastrService
  ) {}

  registerUser(): void {
    this.isLoading = true;
    this._userService
      .registerUser({
        username: this.registerForm.value.username,
        password: this.registerForm.value.password,
        email: this.registerForm.value.email,
      })
      .pipe(finalize(() => (this.isLoading = false)))
      .subscribe({
        next: () => {
          this._router.navigate(['/login']);
          this._toastrService.success('Registration successful');
        },
        error: () => {
          this._toastrService.error('Error occurred while registering');
        },
      });
  }
}
