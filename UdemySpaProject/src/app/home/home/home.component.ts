import { CourseService } from 'src/app/Services/course.service';
import { AuthService } from 'src/app/Services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(private CourseService: CourseService) {}
  ngOnInit(): void {
    this.GetCourses();
  }
  Courses = [];
  TotalCount: number;
  pageSize: number;
  params: any = {};
  GetCourses() {
    this.CourseService.GetCoursePaginated(this.params).subscribe({
      next: (res: any) => {
        this.TotalCount = res.totalCount;
        this.pageSize = res.pageSize;
        this.Courses = res.data;
      },
    });
  }
  onPageChange(event: any): void {
    console.log(event);
    this.params.pageNumber = event.pageIndex + 1;
    this.params.pageSize = event.pageSize;
    this.GetCourses();
  }
}
