import { AuthService } from 'src/app/Services/auth.service';
import { CourseService } from './../../Services/course.service';
import { CourseCategoryService } from './../../Services/course-category.service';
import { Component, OnInit, AfterViewInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-basic-data',
  templateUrl: './basic-data.component.html',
  styleUrls: ['./basic-data.component.css'],
})
export class BasicDataComponent implements OnInit {
  constructor(
    private CourseCategoryService: CourseCategoryService,
    private Router: Router,
    private CourseService: CourseService,
    private AuthService: AuthService
  ) {}

  ngOnInit(): void {
    this.CreateCourseIntro();
    this.GetCategories();
  }

  GetCategories() {
    this.CourseCategoryService.GetCategories().subscribe({
      next: (res: any) => {
        this.Categoeies = res.data;
      },
    });
  }
  selectedTabIndex: number = 0;

  Categoeies = [];
  CourseIntro: FormGroup;

  CreateCourseIntro() {
    this.CourseIntro = new FormGroup({
      Name: new FormControl('', [Validators.required]),
      Category: new FormControl('', [Validators.required]),
      InstructorId: new FormControl(this.AuthService.GetUserId()),
    });
  }
  CreateCourse() {
    // routerLink = '/instructor/coursecreation';
    this.CourseService.CreateBasicCourse(this.CourseIntro.value).subscribe({
      next: (res: any) => {
        this.Router.navigate([`/instructor/coursecreation/${res.data}`]);
      },
    });
  }

  GoPrevios() {
    if (this.selectedTabIndex == 0) return;
    this.selectedTabIndex--;
  }

  Continue() {
    if (this.selectedTabIndex == 1) return;
    this.selectedTabIndex++;
  }
}
