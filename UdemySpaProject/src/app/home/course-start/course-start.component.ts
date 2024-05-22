import { CourseService } from 'src/app/Services/course.service';
import { AuthService } from 'src/app/Services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ReviewComponent } from '../review/review.component';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-course-start',
  templateUrl: './course-start.component.html',
  styleUrls: ['./course-start.component.css'],
})
export class CourseStartComponent {
  constructor(
    private ActivatedRoute: ActivatedRoute,
    private AuthService: AuthService,
    private CourseService: CourseService,
    private Matdialog: MatDialog,
    private sanitizer: DomSanitizer
  ) {}
  ngOnInit(): void {
    this.loadCourseId();
    this.LoadCourseContant();
  }

  courseId: any;
  loadCourseId() {
    this.ActivatedRoute.params.subscribe((params) => {
      this.courseId = params['Id'];
    });
  }

  OpenReviewComponent() {
    this.Matdialog.open(ReviewComponent, {
      minWidth: '70%',
      data: {
        id: this.courseId,
      },
    });
  }

  Rate: any = 4;
  CourseContant: any;
  FirstLectureId: any;
  InstructiorDetails: any;
  AboutCourse: any;
  sicialMeadia: any;
  LoadCourseContant() {
    this.CourseService.LoadCourseContatnt(
      this.AuthService.GetUserId(),
      this.courseId
    ).subscribe({
      next: (res: any) => {
        this.CourseContant = res.data;

        this.AboutCourse = res.data.aboutCourseDto;
        this.InstructiorDetails = this.AboutCourse.instructoreDetails;
        this.sicialMeadia = this.AboutCourse.instructoreDetails.socialAccount;
        console.log(res.data);
        this.PlayCourse(
          res.data.courseContentSection[0].lectureContent[0].lectureId
        );
      },
      error: (err) => {
        this.notFound = true;
      },
    });
  }

  notFound: any = false;
  videoUrl: any;

  PlayCourse(lecturId: any) {
    if (lecturId == null || lecturId == undefined) return;
    this.CourseService.StartCourse(
      this.courseId,
      this.AuthService.GetUserId(),
      lecturId
    ).subscribe({
      next: (response: ArrayBuffer) => {
        const videoBlob = new Blob([response], { type: 'video/mp4' });
        const videoUrl = URL.createObjectURL(videoBlob);
        this.videoUrl = this.sanitizer.bypassSecurityTrustUrl(videoUrl);
      },
      error: (err) => {},
    });
  }
}
