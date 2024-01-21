import { CourseCategoryService } from 'src/app/Services/course-category.service';
import { CourseService } from 'src/app/Services/course.service';
import { AuthService } from 'src/app/Services/auth.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTabChangeEvent, MatTabGroup } from '@angular/material/tabs';
import { Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(
    private CourseService: CourseService,
    private CourseCategoryService: CourseCategoryService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.GetCourses();
    this.LoadCategorises();
  }
  Courses = [];
  TotalCount: number;
  pageSize: number;
  params: any = {};
  GetCourses() {
    this.CourseService.GetCoursePaginated(this.params).subscribe({
      next: (res: any) => {
        this.TotalCount = res.totalCount;
        this.pageSize = res.pageSize;
        this.Courses = res.data;
      },
    });
  }
  categories = [];
  LoadCategorises() {
    this.CourseCategoryService.GetCategories().subscribe({
      next: (res: any) => {
        this.categories = res.data;
      },
    });
  }
  onPageChange(event: any): void {
    this.params.pageNumber = event.pageIndex + 1;
    this.params.pageSize = event.pageSize;
    this.GetCourses();
  }

  Control = new FormControl(null, [Validators.required]);
}
