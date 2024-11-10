import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { CanActivateFn, Router, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, map, Observable, of } from 'rxjs';

export const authGuard: CanActivateFn = (): Observable<boolean | UrlTree> => {
  const router: Router = inject(Router);
  const http: HttpClient = inject(HttpClient);
  const toastrService: ToastrService = inject(ToastrService);

  const url: string = 'https://localhost:7078/api/v1/user/checkToken';

  return http.get<void>(url).pipe(
    map(() => true),
    catchError(() => {
      if (localStorage.getItem('token') !== null) {
        toastrService.error('Session expired. Please login.');
      }
      localStorage.clear();
      return of(router.parseUrl('/login'));
    })
  );
};
