import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CourseLectureService {
  constructor(private HttpClient: HttpClient) {}
  CreateLecture(SectionId) {
    return this.HttpClient.post(
      environment.BaseUrl +
        `CourseLecture/CreateLecture?SectionId=${SectionId}`,
      {}
    );
  }

  UpdateLecture(Lecture) {
    return this.HttpClient.put(
      environment.BaseUrl + `CourseLecture/UpdateLecture`,
      Lecture
    );
  }
  DeleteLecture(LectureId) {
    return this.HttpClient.delete(
      environment.BaseUrl + `CourseLecture/DeleteLecture?LectureId=${LectureId}`
    );
  }
}
