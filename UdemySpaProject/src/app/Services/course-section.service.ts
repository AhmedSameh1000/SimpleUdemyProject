import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CourseSectionService {
  constructor(private HttpClient: HttpClient) {}
  CreateSection(CourseId: any) {
    return this.HttpClient.post(
      `http://localhost:5227/api/CourseSection/CreateSection?CourseId=${CourseId}`,
      {}
    );
  }

  UpdateSection(Section: any) {
    return this.HttpClient.put(
      `http://localhost:5227/api/CourseSection/UpdateSection`,
      Section
    );
  }
  GetSections(CourseId: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/CourseSection/GetSections?CourseId=${CourseId}`
    );
  }
}
