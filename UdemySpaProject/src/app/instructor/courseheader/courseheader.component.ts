import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { PriceService } from './../../Services/price.service';
import { CourseService, MyData } from './../../Services/course.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { RequirmentService } from 'src/app/Services/requirment.service';
import { MessageService } from 'src/app/Services/message.service';
import { ComponentNumbers } from 'src/app/Models/component-numbers';
import { GeneralCourse } from 'src/app/Services/general-course';
import { LandingpageService } from 'src/app/Services/landingpage.service';
import Swal from 'sweetalert2';
import { CurriculmService } from 'src/app/Services/curriculm.service';

@Component({
  selector: 'app-courseheader',
  templateUrl: './courseheader.component.html',
  styleUrls: ['./courseheader.component.css'],
})
export class CourseheaderComponent implements OnInit, OnDestroy {
  constructor(
    private courseService: CourseService,
    private requirmentService: RequirmentService,
    private messageService: MessageService,
    private landingservice: LandingpageService,
    private PriceService: PriceService,
    private CurriculmService: CurriculmService,
    private AuthService: AuthService,
    private Router: Router,
    private ActivatedRoute: ActivatedRoute
  ) {}
  ngOnDestroy(): void {
    if (this.Obs1) {
      this.Obs1.unsubscribe();
    }
  }

  IsCurriculmComponent: boolean;
  Obs1: any;
  ngOnInit(): void {
    this.CurriculmService.GetCurriculmComponent().subscribe({
      next: (IsCurriculmComponent) => {
        this.IsCurriculmComponent = IsCurriculmComponent;
      },
    });

    this.GetCoursId();
    this.Obs1 = this.courseService.GetFiredData().subscribe({
      next: (data) => {
        this.SaveCourse(data.numberObComponent, data);
      },
    });
  }

  CourseId: any;
  GetCoursId() {
    this.ActivatedRoute.paramMap.subscribe({
      next: (data: any) => {
        this.CourseId = +data.get('Id');
      },
    });
  }

  DeleteCourse() {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.courseService
          .DeleteCourse(this.CourseId, this.AuthService.GetUserId())
          .subscribe({
            next: (res) => {
              Swal.fire({
                title: 'Deleted!',
                text: 'Your file has been deleted.',
                icon: 'success',
              });
              this.Router.navigate(['']);
            },
          });
      }
    });
  }
  SaveCourse(numberOfComponent: number, data: MyData) {
    // Choose the service based on NumberOfComponent
    const selectedService: GeneralCourse =
      this.GetSelectedServices(numberOfComponent);

    // Call the SaveCourse method on the selected service
    selectedService.SaveCourse(data);
  }
  GetSelectedServices(numberOfComponent: number): any {
    switch (numberOfComponent) {
      case ComponentNumbers.RequirmentComponentnumber:
        return this.requirmentService;
      case ComponentNumbers.messageComponentnumber:
        return this.messageService;
      case ComponentNumbers.landingpageComponentnumber:
        return this.landingservice;
      case ComponentNumbers.pricingComponentnumber:
        return this.PriceService;
    }
  }

  FireActionSaveClick() {
    this.courseService.FireAction();
  }
}
