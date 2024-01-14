import { CourseService } from 'src/app/Services/course.service';
import { Injectable } from '@angular/core';
import { GeneralCourse } from './general-course';
import { MyData } from './course.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class PriceService implements GeneralCourse {
  constructor(
    private CourseService: CourseService,
    private Toastr: ToastrService
  ) {}
  SaveCourse(formdata: MyData) {
    this.CourseService.UpdateCoursePrice(formdata.Data).subscribe({
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
