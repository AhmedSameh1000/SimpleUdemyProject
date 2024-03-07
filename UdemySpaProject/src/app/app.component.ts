import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from './Services/auth.service';
import { Component, OnInit } from '@angular/core';
import { UserProfileService } from './Services/user-profile.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  constructor(private router: Router, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    console.log(window.location.href);
  }
  title = 'UdemySpaProject';
}
