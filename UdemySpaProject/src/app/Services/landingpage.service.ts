import { Injectable } from '@angular/core';
import { GeneralCourse } from './general-course';
import { CourseService, MyData } from './course.service';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class LandingpageService implements GeneralCourse {
  constructor(
    private CourseService: CourseService,
    private Toastr: ToastrService
  ) {}
  SaveCourse(formdata: MyData) {
    this.CourseService.SaveCourseLandingPage(formdata.Data).subscribe({
      next: (res) => {
        this.Toastr.success('Your changes have been successfully saved.');
        console.log('res here', res);
      },
      error: (err) => {
        this.Toastr.warning(
          'your changes have not been saved please address the issues'
        );
        console.log('here ereer', err);
      },
    });
  }
}
