import { UserProfileService } from './../../Services/user-profile.service';
import { AuthService } from 'src/app/Services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import Swal from 'sweetalert2';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css'],
})
export class ChangePasswordComponent implements OnInit {
  constructor(
    private UserProfileService: UserProfileService,
    private AuthService: AuthService,
    private MatDialogRef: MatDialogRef<ChangePasswordComponent>,
    private Toaster: ToastrService
  ) {}
  ngOnInit(): void {
    this.CreateForm();
  }

  ChangePasswordForm: FormGroup;

  CreateForm() {
    this.ChangePasswordForm = new FormGroup({
      userId: new FormControl(this.AuthService.GetUserId(), [
        Validators.required,
      ]),
      currntPassword: new FormControl(null, [Validators.required]),
      newPassword: new FormControl(null, [Validators.required]),
      confirmPassword: new FormControl(null, [Validators.required]),
    });
  }
  PaswordisWrond: string;
  Changepassword() {
    if (
      this.ChangePasswordForm.get('newPassword').value !=
      this.ChangePasswordForm.get('confirmPassword').value
    ) {
      this.Toaster.warning('new Password And Confirm Password Doesnt Match');
      return;
    }
    this.UserProfileService.ChangePassword(
      this.ChangePasswordForm.value
    ).subscribe({
      next: (res: any) => {
        Swal.fire({
          position: 'top-end',
          icon: 'success',
          title: 'Password Has been Changed',
          showConfirmButton: false,
          timer: 1500,
        });
        this.MatDialogRef.close();
      },
      error: (err) => {
        this.PaswordisWrond = err.error.message;
      },
    });
  }
}
