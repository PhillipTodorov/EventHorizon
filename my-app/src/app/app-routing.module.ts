import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddEventComponent } from './add-event/add-event.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'api/Users/add-event', component: AddEventComponent },
  { path: 'api/Users/login', component: LoginComponent },
  { path: 'api/Users/register', component: RegisterComponent },
  { path: '', redirectTo: '/api/Users/login', pathMatch: 'full' },  // default route
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
