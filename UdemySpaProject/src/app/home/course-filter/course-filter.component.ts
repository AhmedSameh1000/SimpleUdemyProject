import { CourseCategoryService } from 'src/app/Services/course-category.service';
import { CourseService } from 'src/app/Services/course.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatCheckboxChange } from '@angular/material/checkbox';

@Component({
  selector: 'app-course-filter',
  templateUrl: './course-filter.component.html',
  styleUrls: ['./course-filter.component.css'],
})
export class CourseFilterComponent implements OnInit {
  panelOpenState = false;

  constructor(
    private ActivatedRoute: ActivatedRoute,
    private CourseService: CourseService,
    private CourseCategoryService: CourseCategoryService
  ) {}

  ngOnInit(): void {
    this.loadSearchString();

    this.LoadLanguges();
    this.LoadCategories();
  }

  Languges = [];
  LoadLanguges() {
    this.CourseCategoryService.Getlanguges().subscribe({
      next: (res: any) => {
        this.Languges = [{ id: null, name: 'All' }, ...res.data];
      },
    });
  }
  Categories = [];
  LoadCategories() {
    this.CourseCategoryService.GetCategories().subscribe({
      next: (res: any) => {
        this.Categories = [{ id: null, name: 'All' }, ...res.data];
      },
    });
  }
  SearchedCourses = [];

  LoadCourses() {
    this.CourseService.GetCoursePaginated({
      search: this.SearchString,
      langugeId: this.LangugeId,
      categoryId: this.CategoryId,
    }).subscribe({
      next: (res: any) => {
        this.SearchedCourses = res.data;
      },
    });
  }

  SearchString: string;
  loadSearchString() {
    this.ActivatedRoute.queryParamMap.subscribe({
      next: (Res: any) => {
        this.SearchString = Res.get('Search');
        this.LoadCourses();
      },
    });
  }

  LangugeId: number;
  SelectCoursesByLanguge(languageId: any) {
    this.LangugeId = languageId;
    this.LoadCourses();
  }
  CategoryId: number;
  SelectCoursesByCategory(categoryId) {
    this.CategoryId = categoryId;
    this.LoadCourses();
  }
}
