// add-event.component.ts
/*
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EventService } from 'path/to/event.service'; // Import the service to handle event-related operations

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})
export class AddEventComponent implements OnInit {
  eventForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private eventService: EventService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    this.eventForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: [''],
      date: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.eventForm.invalid) {
      return;
    }

    const eventData = {
      title: this.eventForm.value.title,
      description: this.eventForm.value.description,
      date: this.eventForm.value.date
    };

    this.eventService.addEvent(eventData).subscribe(
      response => {
        console.log('Event added successfully:', response);
        // Perform any necessary actions after adding the event (e.g., show success message, redirect)
      },
      error => {
        console.error('Error adding event:', error);
        // Handle any errors that occur during the event addition process (e.g., show error message)
      }
    );
  }
}
*/
