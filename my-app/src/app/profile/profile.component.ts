import { Component, OnInit } from '@angular/core';
import { UserService } from '../User/user.service';
import { User } from '../models/user';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: User;

  constructor(private userService: UserService) {
    this.user = {
      email: '',
      password: '',
      name: '',
      events: [],
    };
  }
  
  ngOnInit() {
    this.userService.getUserProfile().subscribe((user: User) => {
      this.user = user;
    });
  }
}
