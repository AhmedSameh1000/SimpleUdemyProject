import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from './../../Services/auth.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  constructor(private AuthService: AuthService, private Router: Router) {}
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
    this.AuthService.Signup(this.RegisterForm.value).subscribe({
      next: (res: any) => {
        this.AuthService.SaveTokens(res);
        this.Router.navigate(['auth/login']);
      },
      error: (err) => {
        this.Email_is_already_registered = err.error.errors[0];
      },
    });
  }
}
