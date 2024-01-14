import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CourseCategoryService {
  constructor(private HttpClient: HttpClient) {}

  GetCategories() {
    return this.HttpClient.get(
      'http://localhost:5227/api/CourseCategory/Categories'
    );
  }
  Getlanguges() {
    return this.HttpClient.get(
      'http://localhost:5227/api/CourseCategory/Getlanguges'
    );
  }
}
