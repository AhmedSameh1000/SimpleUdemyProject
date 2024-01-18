import { UserProfileService } from './../../Services/user-profile.service';
import { AuthService } from './../../Services/auth.service';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(
    private AuthService: AuthService,

    private Router: Router,
    private UserProfileService: UserProfileService
  ) {}
  ngOnDestroy(): void {
    if (this.Obs1 != null || this.Obs1 != undefined) {
      this.Obs1.unsubscribe();
    }
  }
  ngOnInit(): void {
    this.CreatelogInForm();
  }
  LoginForm: FormGroup;

  CreatelogInForm() {
    this.LoginForm = new FormGroup({
      Email: new FormControl(null, [Validators.required, Validators.email]),
      Password: new FormControl(null, [Validators.required]),
    });
  }
  EmailorPasswordIsIncorrect = '';
  Obs1: any;
  Login() {
    if (this.LoginForm.invalid) {
      return;
    }
    this.Obs1 = this.AuthService.LogIn(this.LoginForm.value).subscribe({
      next: (res: any) => {
        this.AuthService.SaveTokens(res);
        this.UserProfileService.MyImage.next(true);
        this.UserProfileService.GetUserProfileImage(
          this.AuthService.GetUserId()
        ).subscribe({
          next: (res: any) => {
            this.UserProfileService.ChangeImage(res.data);
          },
        });
        this.Router.navigate(['']);
      },
      error: (err) => {
        this.EmailorPasswordIsIncorrect = err.error.message;
      },
    });
  }
}
