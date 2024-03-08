import { CourseCategoryService } from 'src/app/Services/course-category.service';
import { CourseService } from 'src/app/Services/course.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, TitleStrategy } from '@angular/router';
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
    this.CoursesPricesFilteration = [
      { min: 0, max: 200 },
      { min: 200, max: 400 },
      { min: 400, max: 600 },
      { min: 600, max: 800 },
      { min: 800, max: 1000 },
      { min: 1000, max: 1000000 },
    ];
    this.CourseDuration = [
      { min: 1, max: 3 },
      { min: 3, max: 6 },
      { min: 6, max: 11 },
      { min: 11, max: 16 },
      { min: 16, max: 1000000 },
    ];
    this.loadSearchString();
    this.loadCategoryId();
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
      minPrice: this.minPrice,
      maxPrice: this.maxPrice,
      minHours: this.minHours,
      maxHours: this.maxHours,
    }).subscribe({
      next: (res: any) => {
        this.SearchedCourses = res.data;
        console.log(res);
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

  loadCategoryId() {
    this.ActivatedRoute.queryParamMap.subscribe({
      next: (Res: any) => {
        this.CategoryId = Res.get('categoryId');
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

  CoursesPricesFilteration: any;
  maxPrice: any;
  minPrice: any;
  SelectePrice(Price: any) {
    this.maxPrice = Price?.max;
    this.minPrice = Price?.min;
    this.LoadCourses();
  }

  CourseDuration: any;

  minHours: number | null = null;
  maxHours: number | null = null;

  SelecteByDuration(HoursCount: any) {
    if (HoursCount != null) {
      this.maxHours = HoursCount.max * 60;
      this.minHours = HoursCount.min * 60;
    } else {
      this.minHours = null;
      this.maxHours = null;
    }
    this.LoadCourses();
  }
}
