import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  apiUrl = 'https://localhost:7194/api/Events';

  constructor(private http: HttpClient) { }

  // Existing code for adding events
  addEvent(event: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, event);
  }

  // New method for fetching all events
  getEvents(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}
