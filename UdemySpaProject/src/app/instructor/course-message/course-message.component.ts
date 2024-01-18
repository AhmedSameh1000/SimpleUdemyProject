import { ComponentNumbers } from 'src/app/Models/component-numbers';
import { CourseService, MyData } from './../../Services/course.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-course-message',
  templateUrl: './course-message.component.html',
  styleUrls: ['./course-message.component.css'],
})
export class CourseMessageComponent implements OnInit, OnDestroy {
  constructor(
    private CourseService: CourseService,
    private ActivatedRoute: ActivatedRoute,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    localStorage.setItem(
      'SelectedComponent',
      ComponentNumbers.messageComponentnumber.toString()
    );
    this.GetCoursId();
    this.CreateForm();
    this.SubscribeToEventFire();
    this.LoadCourseMessage();
  }
  ngOnDestroy(): void {
    if (this.Obs1) {
      this.Obs1.unsubscribe();
    }
  }
  SubscribeToEventFire() {
    this.Obs1 = this.CourseService.GetCourseFireAction().subscribe({
      next: (res) => {
        var data = new MyData();
        data.isDirty = this.MessagesForm.dirty;
        data.numberObComponent = ComponentNumbers.messageComponentnumber;

        data.Data = this.MessagesForm.value;
        data.CourseId = this.CourseId;
        this.CourseService.SetFiredData(data);
      },
    });
  }

  LoadCourseMessage() {
    this.CourseService.GetCourseMessages(this.CourseId).subscribe({
      next: (res: any) => {
        this.MessagesForm.patchValue({
          WelcomeMessage: res.data.welcomeMessage,
          CongratulationsMessage: res.data.congratulationsMessage,
        });
      },
    });
  }
  Obs1: any;
  MessagesForm: FormGroup;
  CourseId: number;
  CreateForm() {
    this.MessagesForm = new FormGroup({
      WelcomeMessage: new FormControl(null),
      CongratulationsMessage: new FormControl(null),
      courseId: new FormControl(this.CourseId),
      instructorId: new FormControl(this.authService.GetUserId()),
    });
  }
  GetCoursId() {
    this.ActivatedRoute.paramMap.subscribe({
      next: (data: any) => {
        this.CourseId = +data.get('Id');
      },
    });
  }
}
