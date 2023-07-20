import { Component } from '@angular/core';
import { EventService } from '../services/event.service';
interface EventForm {
  name: string;
  date: string;
}

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})

export class AddEventComponent {
  event = {
    name: '',
    date: ''
  };


  constructor(private eventService: EventService) { }

  addEvent(formValues: EventForm) {
    this.eventService.addEvent(formValues).subscribe();
  }

}
