import { ToastrService } from 'ngx-toastr';
import { AuthService } from './../../Services/auth.service';
import { UserProfileService } from './../../Services/user-profile.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-udemyprofile',
  templateUrl: './udemyprofile.component.html',
  styleUrls: ['./udemyprofile.component.css'],
})
export class UdemyprofileComponent implements OnInit {
  constructor(
    private UserProfileService: UserProfileService,
    private AuthService: AuthService,
    private ToastrService: ToastrService
  ) {}
  ngOnInit(): void {
    this.CreateForm();
    this.LoadUserProfile();
  }
  UsemyprofileForm: FormGroup;
  CreateForm() {
    this.UsemyprofileForm = new FormGroup({
      applicationUserId: new FormControl(this.AuthService.GetUserId()),
      FullName: new FormControl(null, [Validators.required]),
      Headline: new FormControl(),
      Biography: new FormControl(),
      WebsiteUrl: new FormControl(),
      TwitterUrl: new FormControl(),
      FacebookUrl: new FormControl(),
      LinkedInUrl: new FormControl(),
      YoutubeUrl: new FormControl(),
    });
  }
  Updateprofile() {
    if (this.UsemyprofileForm.invalid) {
      this.ToastrService.warning(
        'Your changes have not been saved. Please address the issues.'
      );
      return;
    }
    this.UserProfileService.UpdateUserProfile(
      this.UsemyprofileForm.value
    ).subscribe({
      next: (res) => {
        this.UsemyprofileForm.reset({
          applicationUserId: this.AuthService.GetUserId(),
        });
        this.LoadUserProfile();

        this.ToastrService.success(
          'Your changes have been successfully saved.'
        );
      },
    });
  }
  LoadUserProfile() {
    this.UserProfileService.GetUserProfile(
      this.AuthService.GetUserId()
    ).subscribe({
      next: (res: any) => {
        var data = res.data;
        this.UsemyprofileForm.patchValue({
          FullName: data.fullName,
          Headline: data.headline,
          Biography: data.biography,
          WebsiteUrl: data.websiteUrl,
          TwitterUrl: data.twitterUrl,
          FacebookUrl: data.facebookUrl,
          LinkedInUrl: data.linkedInUrl,
          YoutubeUrl: data.youtubeUrl,
        });
      },
      error: (err) => {},
    });
  }
}
