import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Event } from './event.model'; 

@Injectable({
  providedIn: 'root'
})
export class EventService {
  baseUrl = 'https://localhost:7194/api/UserEvents'; // replace this with your actual ASP.NET API endpoint

  constructor(private http: HttpClient) { }
  
  // Method to add a new event
  addEvent(event: Event): Observable<Event> {
      return this.http.post<Event>(this.baseUrl, event);
  }
}
