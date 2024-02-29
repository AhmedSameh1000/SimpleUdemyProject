import { ActivatedRoute } from '@angular/router';
import { CourseService } from 'src/app/Services/course.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-course-details',
  templateUrl: './course-details.component.html',
  styleUrls: ['./course-details.component.css'],
})
export class CourseDetailsComponent implements OnInit {
  constructor(
    private CourseService: CourseService,
    private ActivatedRoute: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.GetCoursId();
    this.LoadCourse();
  }
  CourseDetails: any;

  CourseId: any;
  GetCoursId() {
    this.ActivatedRoute.paramMap.subscribe({
      next: (data: any) => {
        this.CourseId = +data.get('Id');
      },
    });
  }
  LoadCourse() {
    this.CourseService.GetCourseFullDetails(this.CourseId).subscribe({
      next: (res: any) => {
        this.CourseDetails = res.data;
        console.log(res);
      },
    });
  }
  GetLecturesCount() {
    const allLecturesNested = [];
    let count = 0;
    for (const section of this.CourseDetails.contentSections) {
      for (const lecture of section.lectureContent) {
        allLecturesNested.push(lecture);
        count++;
      }
    }
    return count;
  }
  rate = 3;
}
