import { CourseService, MyData } from 'src/app/Services/course.service';
import { Injectable } from '@angular/core';
import { GeneralCourse } from './general-course';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class RequirmentService implements GeneralCourse {
  constructor(
    private CourseService: CourseService,
    private Toastr: ToastrService
  ) {}
  SaveCourse(formdata: MyData) {
    let prerequisiteDTO = {
      id: formdata.CourseId,
      requiments: formdata.Data.Requiments,
      whateYouLearnFromCourse: formdata.Data.WhateYouLearnFromCourse,
      whoIsCourseFor: formdata.Data.WhoIsCourseFor,
    };
    this.CourseService.CreateCourseRequirments(prerequisiteDTO).subscribe({
      next: (res) => {
        this.Toastr.success('Your changes have been successfully saved.');

        console.log(res);
      },
      error: (err) => {
        this.Toastr.warning(
          'your changes have not been saved please address the issues'
        );
        console.log(err);
      },
    });
  }
}
