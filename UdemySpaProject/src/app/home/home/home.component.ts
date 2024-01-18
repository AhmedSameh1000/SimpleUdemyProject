import { CourseService } from 'src/app/Services/course.service';
import { AuthService } from 'src/app/Services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(
    private AuthService: AuthService,
    private CourseService: CourseService
  ) {}
  ngOnInit(): void {
    this.GetCourses();
  }

  Courses = [];
  GetCourses() {
    var params = {};
    this.CourseService.GetCoursePaginated(params).subscribe({
      next: (res: any) => {
        this.Courses = res.data;
      },
    });
  }
}
