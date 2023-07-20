import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AddEventComponent } from './add-event/add-event.component';
import { EventsComponent } from './events/events.component';
import { EventService } from './services/event.service';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ErrorHandlingService } from './services/error-handling.service';
import { HomeComponent } from './home/home.component';  


@NgModule({
  declarations: [
    AppComponent,
    EventsComponent,
    AddEventComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule, 
    AppRoutingModule, 
  ],
  providers: [EventService, ErrorHandlingService], 
  bootstrap: [AppComponent]
})
export class AppModule { }
