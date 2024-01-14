import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CourseLectureService {
  constructor(private HttpClient: HttpClient) {}
  CreateLecture(SectionId) {
    return this.HttpClient.post(
      `http://localhost:5227/api/CourseLecture/CreateLecture?SectionId=${SectionId}`,
      {}
    );
  }

  UpdateLecture(Lecture) {
    return this.HttpClient.post(
      `http://localhost:5227/api/CourseLecture/UpdateLecture`,
      Lecture
    );
  }
}
