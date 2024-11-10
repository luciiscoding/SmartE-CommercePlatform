import { inject } from '@angular/core';
import { CanActivateFn, Router, UrlTree } from '@angular/router';
import { Observable, of } from 'rxjs';

export const authGuard: CanActivateFn = (): Observable<boolean | UrlTree> => {
  const router = inject(Router);

  return sessionStorage.getItem('userId')
    ? of(true)
    : of(router.parseUrl('/login'));
};
