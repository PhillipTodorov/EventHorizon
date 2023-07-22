import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class ErrorHandlingService {
    constructor(){}
    
    determineErrorMessage(error: any): string {
        // First, check for the duplicate email case
        if (error.error && error.error[''] && error.error[''][0]) {
            let errorMessage = error.error[''][0];
            if (errorMessage.includes('Email already in use')) {
                return 'This email is already in use.';
            }
        }
        // Then, check for the other validation errors
        else if (error.error && error.error.errors) {
            let errorDetails = error.error.errors;
    
            if (errorDetails.Email) {
                let errorMessage = errorDetails.Email[0];
                if (errorMessage.includes('Email')) {
                    return 'Please enter a valid email.';
                }
            }
    
            else if (errorDetails.Password) {
                let errorMessage = errorDetails.Password[0];
                if (errorMessage.includes('Password')) {
                    return 'Password must meet the minimum length and complexity requirements.';
                }
            }
        }
    
        return 'Unknown error occurred during registration.';
    }
    
}
