import { AuthService } from 'src/app/Services/auth.service';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ChangePasswordComponent } from '../change-password/change-password.component';

@Component({
  selector: 'app-accountsecurity',
  templateUrl: './accountsecurity.component.html',
  styleUrls: ['./accountsecurity.component.css'],
})
export class AccountsecurityComponent {
  constructor(private Dialog: MatDialog, private AuthService: AuthService) {}

  OpenChangePassword() {
    var AfterClose = this.Dialog.open(ChangePasswordComponent, {
      minWidth: '50%',
      minHeight: '60%',
      disableClose: true,
    });
  }
  Email: any;
  ngOnInit(): void {
    this.Email = this.AuthService.GetEmail();
  }
}
