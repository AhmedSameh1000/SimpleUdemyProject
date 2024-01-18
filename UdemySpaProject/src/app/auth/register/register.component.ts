import { UserProfileService } from './../../Services/user-profile.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from './../../Services/auth.service';
import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnDestroy {
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

  Obs1: any;
  ngOnInit(): void {
    this.CreatelogInForm();
  }
  RegisterForm: FormGroup;

  CreatelogInForm() {
    this.RegisterForm = new FormGroup({
      FullName: new FormControl('', [Validators.required]),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', [Validators.required]),
    });
  }

  Email_is_already_registered = '';
  Register() {
    if (this.RegisterForm.invalid) {
      return;
    }
    this.Obs1 = this.AuthService.Signup(this.RegisterForm.value).subscribe({
      next: (res: any) => {
        // this.AuthService.SaveTokens(res);
        this.Router.navigate(['auth/login']);
      },
      error: (err) => {
        this.Email_is_already_registered = err.error.message;
      },
    });
  }
}
