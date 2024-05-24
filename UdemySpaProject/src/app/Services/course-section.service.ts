import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CourseSectionService {
  constructor(private HttpClient: HttpClient) {}
  CreateSection(CourseId: any) {
    return this.HttpClient.post(
      environment.BaseUrl + `CourseSection/CreateSection?CourseId=${CourseId}`,
      {}
    );
  }
  DeleteSection(SectionId: any) {
    return this.HttpClient.delete(
      environment.BaseUrl +
        `CourseSection/DeletedSection?SectionId=${SectionId}`
    );
  }

  UpdateSection(Section: any) {
    return this.HttpClient.put(
      environment.BaseUrl + `CourseSection/UpdateSection`,
      Section
    );
  }
  GetSections(CourseId: any) {
    return this.HttpClient.get(
      environment.BaseUrl + `CourseSection/GetSections?CourseId=${CourseId}`
    );
  }
}
