import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { ProfileComponent } from './profile/profile.component';
import { MaterialModule } from '../material/material.module';
import { UdemyprofileComponent } from './udemyprofile/udemyprofile.component';
import { ProfilepictureComponent } from './profilepicture/profilepicture.component';
import { AccountsecurityComponent } from './accountsecurity/accountsecurity.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ProfileComponent,
    UdemyprofileComponent,
    ProfilepictureComponent,
    AccountsecurityComponent,
    ChangePasswordComponent,
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
  ],
})
export class UserModule {}
