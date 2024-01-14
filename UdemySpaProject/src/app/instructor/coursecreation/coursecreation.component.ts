import { ComponentNumbers } from 'src/app/Models/component-numbers';
import { CourseService } from './../../Services/course.service';
import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-coursecreation',
  templateUrl: './coursecreation.component.html',
  styleUrls: ['./coursecreation.component.css'],
})
export class CoursecreationComponent implements OnInit, OnDestroy {
  constructor(private CourseService: CourseService) {
    if (
      localStorage.getItem('SelectedComponent') === null ||
      localStorage.getItem('SelectedComponent') === undefined
    ) {
      this.SelectedComponent = ComponentNumbers.RequirmentComponentnumber;
    } else {
      this.SelectedComponent = +localStorage.getItem('SelectedComponent');
    }
  }
  ngOnDestroy(): void {
    localStorage.setItem(
      'SelectedComponent',
      ComponentNumbers.RequirmentComponentnumber.toString()
    );
  }
  ngOnInit(): void {
    this.CourseService;
    this.CourseService.GetComponentNumber().subscribe({
      next: (res) => {
        this.SelectedComponent = res;
      },
    });
  }
  SelectedComponent: number;
}
