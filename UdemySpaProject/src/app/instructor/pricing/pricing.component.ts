import { AuthService } from './../../Services/auth.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ComponentNumbers } from 'src/app/Models/component-numbers';
import { CourseService, MyData } from 'src/app/Services/course.service';

@Component({
  selector: 'app-pricing',
  templateUrl: './pricing.component.html',
  styleUrls: ['./pricing.component.css'],
})
export class PricingComponent implements OnInit, OnDestroy {
  constructor(
    private CourseService: CourseService,
    private ActivatedRoute: ActivatedRoute,
    private AuthService: AuthService
  ) {}
  ngOnDestroy(): void {
    if (this.Obs1) {
      this.Obs1.unsubscribe();
    }
  }
  ngOnInit(): void {
    localStorage.setItem(
      'SelectedComponent',
      ComponentNumbers.pricingComponentnumber.toString()
    );
    this.GetCoursId();
    this.CreatePriceForm();
    this.SavePriceData();
    this.LoadCoursePrice();
  }

  LoadCoursePrice() {
    this.CourseService.GetCoursPrice(this.CourseId).subscribe({
      next: (res: any) => {
        this.PriceForm.patchValue({
          Price: res.data.price,
        });
      },
    });
  }
  Obs1: any;
  SavePriceData() {
    this.Obs1 = this.CourseService.GetCourseFireAction().subscribe({
      next: (res) => {
        var data = new MyData();
        data.Data = this.PriceForm.value;
        data.CourseId = this.CourseId;
        data.isDirty = this.PriceForm.dirty;
        data.numberObComponent = ComponentNumbers.pricingComponentnumber;
        this.CourseService.SetFiredData(data);
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

  PriceForm: FormGroup;

  CreatePriceForm() {
    this.PriceForm = new FormGroup({
      Price: new FormControl(null),
      CourseId: new FormControl(this.CourseId),
      InstructorId: new FormControl(this.AuthService.GetUserId()),
    });
  }
}
