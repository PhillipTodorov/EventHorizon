import { Component, OnInit } from '@angular/core';
import { UserService } from '../User/user.service';
import { Router } from '@angular/router';
import { ErrorHandlingService } from '../services/error-handling.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  email: string;
  password: string;

  constructor(
    private userService: UserService, 
    private router: Router,
    private errorHandlingService: ErrorHandlingService
  ) {
    this.email = '';
    this.password = '';
  }

  ngOnInit(): void {
  }

  onRegister(): void {
    this.userService.register(this.email, this.password).subscribe({
      next: data => {
        // on success, redirect to login page
        this.router.navigate(['/api/Users/login']);
        alert('Registration successful!'); 
      },
      error: error => {
        console.log(error); // This line logs the full error object to the console.
        if (error.status === 400) {
          alert(this.errorHandlingService.determineErrorMessage(error)); 
        } else {
          console.error('Error in register', error);
        }
      }
    });
  } 
        
}
