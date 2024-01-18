import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserProfileService {
  constructor(private HttpClient: HttpClient) {}

  UpdateUserProfile(userprofile) {
    return this.HttpClient.put(
      'http://localhost:5227/api/UserProfile/UpdateUserProfile',
      userprofile
    );
  }
  ChangePassword(PasswordModel) {
    return this.HttpClient.put(
      'http://localhost:5227/api/UserProfile/ChangePassword',
      PasswordModel
    );
  }
  UploadUserImage(UserImage) {
    return this.HttpClient.put(
      'http://localhost:5227/api/UserProfile/UploadUserImage',
      UserImage
    );
  }
  GetUserProfile(userId) {
    return this.HttpClient.get(
      `http://localhost:5227/api/UserProfile/GetuserProfile?userId=${userId}`
    );
  }
  GetUserProfileImage(userId) {
    return this.HttpClient.get(
      `http://localhost:5227/api/UserProfile/GetImageProfile?userId=${userId}`
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
