import { CourseCategoryService } from 'src/app/Services/course-category.service';
import { UserProfileService } from './../../Services/user-profile.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  constructor(
    public AuthService: AuthService,
    private UserProfileService: UserProfileService,
    private CourseCategoryService: CourseCategoryService
  ) {}
  ngOnInit(): void {
    this.LoaduserProfileImage();
    this.IsImageProfileChanges();
    this.LoadCategories();
  }

  IsImageProfileChanges() {
    this.UserProfileService.GetChangeImageEvent().subscribe({
      next: () => {
        this.LoaduserProfileImage();
      },
    });
  }
  CategoryList = [];
  LoadCategories() {
    this.CourseCategoryService.GetCategories().subscribe({
      next: (res: any) => {
        this.CategoryList = res.data;
      },
    });
  }
  Image: any;
  LoaduserProfileImage() {
    this.UserProfileService.GetUserProfileImage(
      this.AuthService.GetUserId()
    ).subscribe({
      next: (res: any) => {
        this.Image = res.data;
      },
    });
  }
}
