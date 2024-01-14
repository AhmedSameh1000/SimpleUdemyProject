import { CourseService } from 'src/app/Services/course.service';
import { MyData } from './course.service';
import { GeneralCourse } from './general-course';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class MessageService implements GeneralCourse {
  constructor(
    private CourseService: CourseService,
    private Toastr: ToastrService
  ) {}
  SaveCourse(formdata: MyData) {
    this.CourseService.UpdateCourseMessages(formdata.Data).subscribe({
      next: (res) => {
        this.Toastr.success('Your changes have been successfully saved.');
      },
      error: (err) => {
        this.Toastr.warning(
          'your changes have not been saved please address the issues'
        );
      },
    });
  }
}
