import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CourseCategoryService {
  constructor(private HttpClient: HttpClient) {}

  GetCategories() {
    return this.HttpClient.get(
      environment.BaseUrl + 'CourseCategory/Categories'
    );
  }
  Getlanguges() {
    return this.HttpClient.get(
      environment.BaseUrl + 'CourseCategory/Getlanguges'
    );
  }
}
