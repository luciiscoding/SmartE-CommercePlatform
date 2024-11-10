import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '@app/shared';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  options: {
    headers: HttpHeaders;
  } = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  onUserLogin$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  private _baseUrl: string = 'https://localhost:7078/api/v1/user';

  constructor(private _httpClient: HttpClient) {}

  loginUser(user: User): Observable<string> {
    return this._httpClient.post<string>(
      `${this._baseUrl}/login`,
      user,
      this.options
    );
  }

  registerUser(user: User): Observable<string> {
    return this._httpClient.post<string>(
      `${this._baseUrl}/register`,
      user,
      this.options
    );
  }

  getUserById(userId: string): Observable<User> {
    return this._httpClient.get<User>(
      `${this._baseUrl}/${userId}`,
      this.options
    );
  }
}
