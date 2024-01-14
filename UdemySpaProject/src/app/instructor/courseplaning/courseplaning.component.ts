import { CourseService } from './../../Services/course.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-courseplaning',
  templateUrl: './courseplaning.component.html',
  styleUrls: ['./courseplaning.component.css'],
})
export class CourseplaningComponent {
  constructor(private CourseService: CourseService) {}
  RedirectTo(numberOfComponent: number) {
    this.CourseService.EmitComponentNumber(numberOfComponent);
  }
}
