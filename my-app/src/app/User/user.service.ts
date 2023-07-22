import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };

    const body = {
      email: email,
      password: password
    };

    return this.http.post<any>('https://localhost:7194/api/Users/login', body, httpOptions);
  }

  register(email: string, password: string): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };

    const body = {
      email: email,
      password: password
    };

    return this.http.post<any>('https://localhost:7194/api/Users/register', body, httpOptions);
  }

  getUserProfile(): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        // Include Authorization header if needed
      }),
    };

    return this.http.get<any>('https://localhost:7194/api/Users/profile', httpOptions);
  }
}
