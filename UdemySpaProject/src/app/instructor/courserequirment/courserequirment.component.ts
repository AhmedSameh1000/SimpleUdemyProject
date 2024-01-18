import { CourseService, MyData } from './../../Services/course.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ComponentNumbers } from 'src/app/Models/component-numbers';
@Component({
  selector: 'app-courserequirment',
  templateUrl: './courserequirment.component.html',
  styleUrls: ['./courserequirment.component.css'],
})
export class CourserequirmentComponent implements OnInit, OnDestroy {
  constructor(
    private CourseService: CourseService,
    private ActivatedRoute: ActivatedRoute,
    private Toastr: ToastrService
  ) {}
  ngOnDestroy(): void {
    if (this.Obs1) {
      this.Obs1.unsubscribe();
    }
  }

  Obs1: any;
  ngOnInit(): void {
    localStorage.setItem(
      'SelectedComponent',
      ComponentNumbers.RequirmentComponentnumber.toString()
    );
    this.CreateCoursePrerequisiteForm();
    this.GetCoursId();
    this.LoadCourses();
    this.SaveRequirmentWhenFireAction();
  }
  SaveRequirmentWhenFireAction() {
    this.Obs1 = this.CourseService.GetCourseFireAction().subscribe({
      next: (res) => {
        var data = new MyData();
        data.Data = this.PrerequisiteForm.value;
        data.isDirty = this.PrerequisiteForm.dirty;
        data.CourseId = this.CourseId;
        data.numberObComponent = ComponentNumbers.RequirmentComponentnumber;
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
  Remove(Index: number, arrayname: string) {
    var FormArray = this.PrerequisiteForm.get(arrayname) as FormArray;
    FormArray.removeAt(Index);
  }
  LoadCourses() {
    this.CourseService.GetCourseDetails(this.CourseId).subscribe({
      next: (res: any) => {
        this.SetValuesToFormControl(res.data.requirments, 'Requiments');
        this.SetValuesToFormControl(
          res.data.whateWillYouLearnFromCourse,
          'WhateYouLearnFromCourse'
        );
        this.SetValuesToFormControl(
          res.data.whoIsThisCourseFor,
          'WhoIsCourseFor'
        );
      },
    });
  }

  SetValuesToFormControl(Values: any[], ArrayControls: string) {
    if (Values.length == 0) {
      return;
    }
    var FormArray = this.PrerequisiteForm.get(ArrayControls) as FormArray;
    var LengthOfArray = FormArray.value.length - 1;

    while (FormArray.length) {
      FormArray.removeAt(LengthOfArray--);
    }

    Values.forEach((arr) => {
      FormArray.push(new FormControl(arr.name, [Validators.required]));
    });
  }

  PrerequisiteForm: FormGroup;

  CreateCoursePrerequisiteForm() {
    this.PrerequisiteForm = new FormGroup({
      WhateYouLearnFromCourse: new FormArray([
        new FormControl(null, [Validators.required]),
        new FormControl(null, [Validators.required]),
        new FormControl(null, [Validators.required]),
      ]),
      Requiments: new FormArray([new FormControl(null, [Validators.required])]),
      WhoIsCourseFor: new FormArray([
        new FormControl(null, [Validators.required]),
      ]),
    });
  }

  AddMoreToYourResponse(FormArrayName: string) {
    var FormArray = this.PrerequisiteForm.get(FormArrayName) as FormArray;

    if (FormArray.invalid) {
      return;
    }
    FormArray.push(new FormControl(null, [Validators.required]));
  }
}
