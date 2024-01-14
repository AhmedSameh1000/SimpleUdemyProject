import { Injectable } from '@angular/core';
import { GeneralCourse } from './general-course';
import { MyData } from './course.service';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CurriculmService implements GeneralCourse {
  constructor() {}
  SaveCourse(formdata: MyData) {
    throw new Error('Method not implemented.');
  }
  IsCurriculm: Subject<any> = new Subject();

  ISCurriculmComponent(value: boolean) {
    this.IsCurriculm.next(value);
  }
  GetCurriculmComponent() {
    return this.IsCurriculm.asObservable();
  }
}
