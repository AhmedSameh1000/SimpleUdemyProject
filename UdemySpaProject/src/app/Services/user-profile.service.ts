import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class UserProfileService {
  constructor(private HttpClient: HttpClient) {}

  UpdateUserProfile(userprofile) {
    return this.HttpClient.put(
      environment.BaseUrl + 'UserProfile/UpdateUserProfile',
      userprofile
    );
  }
  ChangePassword(PasswordModel) {
    return this.HttpClient.put(
      environment.BaseUrl + 'UserProfile/ChangePassword',
      PasswordModel
    );
  }
  UploadUserImage(UserImage) {
    return this.HttpClient.put(
      environment.BaseUrl + 'UserProfile/UploadUserImage',
      UserImage
    );
  }
  GetUserProfile(userId) {
    return this.HttpClient.get(
      environment.BaseUrl + `UserProfile/GetuserProfile?userId=${userId}`
    );
  }
  GetUserProfileImage(userId) {
    return this.HttpClient.get(
      environment.BaseUrl + `UserProfile/GetImageProfile?userId=${userId}`
    );
  }

  MyImage = new Subject<boolean>();

  private ImageChanges = new Subject();

  ChangeImage(url) {
    this.ImageChanges.next(url);
  }
  GetChangeImageEvent() {
    return this.ImageChanges.asObservable();
  }
}
