import { CourseService } from 'src/app/Services/course.service';
import { Router } from '@angular/router';
import { CourseCategoryService } from 'src/app/Services/course-category.service';
import { UserProfileService } from './../../Services/user-profile.service';
import {
  ChangeDetectorRef,
  Component,
  DoCheck,
  OnDestroy,
  OnInit,
  Renderer2,
} from '@angular/core';
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
    private CourseCategoryService: CourseCategoryService,
    private CourseService: CourseService,
    private Router: Router,
    private renderer: Renderer2
  ) {
    this.LoaduserProfileImage();
  }

  Obs1: any;
  Obs2: any;
  Obs3: any;

  ngOnInit(): void {
    this.UserProfileService.MyImage.asObservable().subscribe({
      next: (res) => {
        if (res) {
          this.LoaduserProfileImage();
        }
      },
    });
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
    if (!this.AuthService.isLoggedIn()) return;
    this.UserProfileService.GetUserProfileImage(
      this.AuthService.GetUserId()
    ).subscribe({
      next: (res: any) => {
        this.Image = res.data;
      },
      error: (err) => {
        this.Image = '../../../assets/profileimagedefault/download.png';
      },
    });
  }
  logOut() {
    this.AuthService.logout();
    this.Router.navigate(['']);
  }
  OnSearch(value) {
    this.Router.navigate(['/filterCourses'], {
      queryParams: { Search: value },
    });
  }
  SendCategoryIdToSearch(CategoryId) {
    this.Router.navigate(['/filterCourses'], {
      queryParams: { categoryId: CategoryId },
    });
  }
}
