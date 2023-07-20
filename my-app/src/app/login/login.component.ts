import { Component, OnInit } from '@angular/core';
import { UserService } from '../User/user.service';
import { Router } from '@angular/router';
import { ErrorHandlingService } from '../services/error-handling.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
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

  login(): void {
    this.userService.login(this.email, this.password).subscribe(
      data => {
        // on success, redirect to home page
        this.router.navigate(['/home']);
      },
      error => {
        if (error.status === 400) {
          alert(this.errorHandlingService.determineErrorMessage(error)); 
        } else {
          console.error('Error in login', error);
        }
      }
    );
  }
}
