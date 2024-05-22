import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { Component, OnInit } from '@angular/core';
import { CourseService } from 'src/app/Services/course.service';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css'],
})
export class LandingComponent implements OnInit {
  constructor(
    private AuthService: AuthService,
    private CourseseService: CourseService,
    private Router: Router
  ) {}
  ngOnInit(): void {
    this.LoadCourses();
  }
  Courses = [];

  LoadCourses() {
    this.CourseseService.GetInstructorCourses(
      this.AuthService.GetUserId()
    ).subscribe({
      next: (Res: any) => {
        this.Courses = Res.data;
      },
    });
  }
  GoToCourseCreation(CourseId: any, title: any) {
    this.Router.navigate([`/instructor/coursecreation/${CourseId}`]);
  }
}
