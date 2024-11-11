import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const request: HttpRequest<unknown> = req.clone({
      headers: req.headers.append(
        'Authorization',
        `Bearer ${localStorage.getItem('token')}`
      ),
    });
    return next.handle(request);
  }
}
