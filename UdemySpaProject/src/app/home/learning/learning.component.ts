import { MatDialog } from '@angular/material/dialog';
import { CourseService } from 'src/app/Services/course.service';
import { AuthService } from 'src/app/Services/auth.service';
import { Component, OnInit } from '@angular/core';
import { ReviewComponent } from '../review/review.component';

@Component({
  selector: 'app-learning',
  templateUrl: './learning.component.html',
  styleUrls: ['./learning.component.css'],
})
export class LearningComponent implements OnInit {
  constructor(
    private AuthService: AuthService,
    private CourseService: CourseService,
    private MatDialog: MatDialog
  ) {}
  ngOnInit(): void {
    this.loadMylearningCourses();
  }
  Courses: any;
  loadMylearningCourses() {
    this.CourseService.GetMyLearning(this.AuthService.GetUserId()).subscribe({
      next: (res: any) => {
        this.Courses = res.data;
        console.log(this.Courses);
      },
    });
  }

  OpenReviewComponent(courseId: any) {
    this.MatDialog.open(ReviewComponent, {
      minWidth: '70%',
      data: {
        id: courseId,
      },
    });
  }
}
