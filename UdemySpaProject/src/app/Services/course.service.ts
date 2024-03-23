import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CourseService {
  constructor(private HttpClient: HttpClient) {
    this.FiredData = new Subject<MyData>(); // Initialize FiredData here
  }

  ComponentRedirection = new Subject<number>();
  CourseActionFire = new Subject<void>();
  FiredData: Subject<MyData>; // Initialized in the constructor

  EmitComponentNumber(ComponentNumber: number) {
    this.ComponentRedirection.next(ComponentNumber);
  }

  GetComponentNumber(): Subject<number> {
    return this.ComponentRedirection;
  }

  GetCourseFireAction() {
    return this.CourseActionFire.asObservable();
  }

  FireAction() {
    return this.CourseActionFire.next();
  }

  CreateBasicCourse(BasicCourse: any) {
    return this.HttpClient.post(
      'http://localhost:5227/api/Course/CreateBasicCourse',
      BasicCourse
    );
  }
  UpdateCourseMessages(CourseMessages: any) {
    return this.HttpClient.post(
      'http://localhost:5227/api/Course/UpdateCourseMessage',
      CourseMessages
    );
  }

  GetInstructorCourses(userId: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/Course/InstructorCourss?InstructorId=${userId}`
    );
  }
  GetCourseFullDetails(courseId: any, userId: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/Course/GetCourseFullDetails?CourseId=${courseId}&userId=${userId}`
    );
  }
  InrollFreeCourse(courseId: any, userId: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/Course/InrollFreeCourse?CourseId=${courseId}&userId=${userId}`
    );
  }
  SaveCourseLandingPage(CourseLandingPage: any) {
    return this.HttpClient.post(
      'http://localhost:5227/api/Course/SaveCourseLanding',
      CourseLandingPage
    );
  }
  UpdateCoursePrice(CourseForUpdate: any) {
    return this.HttpClient.post(
      'http://localhost:5227/api/Course/UpdateCoursePrice',
      CourseForUpdate
    );
  }

  CreateCourseRequirments(Requirments: any) {
    return this.HttpClient.post(
      'http://localhost:5227/api/Course/CreateRequirmentCourse',
      Requirments
    );
  }

  GetCourseDetails(Id: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/Course/GetCourseDetails?Id=${Id}`
    );
  }

  GetCourseMessages(Id: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/Course/CourseMessages?Id=${Id}`
    );
  }
  GetCourseLandingPage(Id: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/Course/CourseLandingPage?Id=${Id}`
    );
  }
  GetMyLearning(Id: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/Course/GetMyLearning?userId=${Id}`
    );
  }

  DeleteCourse(CourseId: any, InstructorId: any) {
    return this.HttpClient.delete(
      `http://localhost:5227/api/Course/DeleteCourse?CourseId=${CourseId}&InstructorId=${InstructorId}`
    );
  }

  GetCoursPrice(Id: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/Course/GetCoursePrice?CourseId=${Id}`
    );
  }
  GetCoursePaginated(QueryPaginateion: any) {
    let paginationQuery = new HttpParams();

    if (QueryPaginateion.pageSize != null) {
      paginationQuery = paginationQuery.set(
        'pageSize',
        QueryPaginateion.pageSize
      );
    }
    if (QueryPaginateion.pageNumber != null) {
      paginationQuery = paginationQuery.set(
        'pageNumber',
        QueryPaginateion.pageNumber
      );
    }
    if (QueryPaginateion.search != null) {
      paginationQuery = paginationQuery.set('search', QueryPaginateion.search);
    }
    if (QueryPaginateion.langugeId != null) {
      paginationQuery = paginationQuery.set(
        'langugeId',
        QueryPaginateion.langugeId
      );
    }
    if (QueryPaginateion.categoryId != null) {
      paginationQuery = paginationQuery.set(
        'categoryId',
        QueryPaginateion.categoryId
      );
    }
    if (QueryPaginateion.minPrice != null) {
      paginationQuery = paginationQuery.set(
        'minPrice',
        QueryPaginateion.minPrice
      );
    }
    if (QueryPaginateion.maxPrice != null) {
      paginationQuery = paginationQuery.set(
        'maxPrice',
        QueryPaginateion.maxPrice
      );
    }
    if (QueryPaginateion.minHours != null) {
      paginationQuery = paginationQuery.set(
        'minHours',
        QueryPaginateion.minHours
      );
    }
    if (QueryPaginateion.maxHours != null) {
      paginationQuery = paginationQuery.set(
        'maxHours',
        QueryPaginateion.maxHours
      );
    }

    return this.HttpClient.get(
      `http://localhost:5227/api/Course/GetCoursePaginated`,
      { params: paginationQuery }
    );
  }

  GetStreamVideoPromotion(Id: any): Observable<ArrayBuffer> {
    return this.HttpClient.get(
      `http://localhost:5227/api/Course/StreamVideoPromotion?Id=${Id}`,
      {
        responseType: 'arraybuffer',
      }
    );
  }

  StartCourse(
    courseId: any,
    userId: any,
    lectureId: any
  ): Observable<ArrayBuffer> {
    return this.HttpClient.get(
      `http://localhost:5227/api/Course/StartCourse?userId=${userId}&courseId=${courseId}&lectureId=${lectureId}`,
      {
        responseType: 'arraybuffer',
      }
    );
  }

  LoadCourseContatnt(userId: any, courseId: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/Course/CourseContant?userId=${userId}&courseId=${courseId}`
    );
  }
  SetFiredData(Data: MyData) {
    this.FiredData.next(Data);
  }

  GetFiredData() {
    return this.FiredData.asObservable();
  }
}

export class MyData {
  Data: any;
  isDirty = false;
  CourseId: number;
  numberObComponent: number;
  constructor() {}
}
