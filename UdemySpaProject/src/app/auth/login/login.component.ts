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
  constructor(private AuthService: AuthService, private Router: Router) {}
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
  Login() {
    if (this.LoginForm.invalid) {
      return;
    }
    this.AuthService.LogIn(this.LoginForm.value).subscribe({
      next: (res: any) => {
        this.AuthService.SaveTokens(res);
        this.Router.navigate(['']);
      },
      error: (err) => {
        this.EmailorPasswordIsIncorrect = err.error.errors[0];
      },
    });
  }
}
