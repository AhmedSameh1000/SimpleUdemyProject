import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/Services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { CourseService } from './../../Services/course.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-courseplaning',
  templateUrl: './courseplaning.component.html',
  styleUrls: ['./courseplaning.component.css'],
})
export class CourseplaningComponent implements OnInit {
  constructor(
    private CourseService: CourseService,
    private ActivatedRoute: ActivatedRoute,
    private AuthService: AuthService,
    private ToastrService: ToastrService
  ) {}
  ngOnInit(): void {
    this.LoadCourseId();
  }
  RedirectTo(numberOfComponent: number) {
    this.CourseService.EmitComponentNumber(numberOfComponent);
  }

  CourseId: any;
  LoadCourseId() {
    this.ActivatedRoute.paramMap.subscribe({
      next: (res) => {
        this.CourseId = res.get('Id');
      },
    });
  }
  TryPublishCourse() {
    this.CourseService.TrypublishCoure(
      this.AuthService.GetUserId(),
      this.CourseId
    ).subscribe({
      next: (res) => {
        this.ToastrService.success('Course Published Successfuly');
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
