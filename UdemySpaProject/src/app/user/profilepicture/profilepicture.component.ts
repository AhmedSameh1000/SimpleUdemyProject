import { UserProfileService } from './../../Services/user-profile.service';
import { AuthService } from './../../Services/auth.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profilepicture',
  templateUrl: './profilepicture.component.html',
  styleUrls: ['./profilepicture.component.css'],
})
export class ProfilepictureComponent implements OnInit {
  constructor(
    private Toaster: ToastrService,
    private AuthService: AuthService,
    private UserProfileService: UserProfileService
  ) {}
  ngOnInit(): void {
    this.LoadUserImage();
  }
  SaveImage() {
    if (this.File == null || this.File == undefined) {
      return;
    }
    let UserImage = new FormData();
    if (this.File != null || this.File != undefined) {
      UserImage.append('Image', this.File, this.File.name);
      UserImage.append('userId', this.AuthService.GetUserId());
      this.UserProfileService.UploadUserImage(UserImage).subscribe({
        next: (res) => {
          this.UserProfileService.ChangeImage('');
        },
      });
    }
  }
  UserImage: any;
  LoadUserImage() {
    this.UserProfileService.GetUserProfileImage(
      this.AuthService.GetUserId()
    ).subscribe({
      next: (res: any) => {
        this.UserImage = res.data;
      },
    });
  }

  File: any;
  AllowdExtens = ['png', 'jpg', 'jpeg'];
  SelectImage(event, img: HTMLImageElement) {
    if (!this.AllowdExtens.includes(event.target.files[0].type.split('/')[1])) {
      this.Toaster.warning('Choose Valid Image');
      return;
    }
    this.File = event.target.files[0];
    var ext = this.File.name.split('.')[1];

    const objectURL = URL.createObjectURL(this.File);
    img.src = objectURL;
  }
}
