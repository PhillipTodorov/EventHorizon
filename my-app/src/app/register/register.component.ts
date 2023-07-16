import { Component, OnInit } from '@angular/core';
import { UserService } from '../User/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  email: string;
  password: string;

  constructor(private userService: UserService, private router: Router) {
    this.email = '';
    this.password = '';
}

  ngOnInit(): void {
  }

  onRegister(): void {
    if (!this.email || !this.password) {
      alert('Email or Password field cannot be empty.'); // replace alert with your own popup implementation
      return;
    }
    
    this.userService.register(this.email, this.password).subscribe(
      data => {
        // on success, redirect to login page
        this.router.navigate(['/api/Users/login']);
        alert('Registration successful!'); 
      },
      error => {
        if (error.status === 400) {
          if (error.error === 'Email already in use') {
            alert('This email is already in use.'); 
          } else if (error.error === 'Invalid email format') {
            alert('Please enter a valid email.'); 
          } else if (error.error === 'Password does not meet requirements') {
            alert('Password must meet the minimum length and complexity requirements.'); 
          } else {
            alert('Unknown error occurred during registration.'); 
          }
        } else {
          console.error('Error in register', error);
        }
      }
    );
  }
  
}
