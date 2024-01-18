import { AuthService } from './Services/auth.service';
import { Component, OnInit } from '@angular/core';
import { UserProfileService } from './Services/user-profile.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  constructor(
    private userProfile: UserProfileService,
    private AuthService: AuthService
  ) {}
  ngOnInit(): void {}
  title = 'UdemySpaProject';
}
