import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  title = 'Welcome to Event Horizon';
  subtitle = 'Plan your events with ease';
  content = `Here at Event Horizon, we are dedicated to helping you plan and organize your events. Whether it's a small meeting or a large conference, we've got you covered. Start planning your event today!`;
  buttonText = 'LEARN MORE';
}
