import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ComponentNumbers } from 'src/app/Models/component-numbers';
import { CourseCategoryService } from 'src/app/Services/course-category.service';
import { CourseService, MyData } from 'src/app/Services/course.service';

@Component({
  selector: 'app-landingpage',
  templateUrl: './landingpage.component.html',
  styleUrls: ['./landingpage.component.css'],
})
export class LandingpageComponent implements OnInit, OnDestroy {
  landingForm: FormGroup;

  constructor(
    private CourseService: CourseService,
    private CourseCategoryService: CourseCategoryService,
    private ActivatedRoute: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private Toastr: ToastrService
  ) {}
  ngOnInit(): void {
    localStorage.setItem(
      'SelectedComponent',
      ComponentNumbers.landingpageComponentnumber.toString()
    );
    this.CreateLandingForm();
    this.GetCoursId();
    this.SaveLandingpageData();
    this.GetCategories();
    this.Getlanguges();
    this.LoadCourseLandingPage();
    this.LoadStreamVideoPromotion();
  }
  ngOnDestroy(): void {
    if (this.Obs1) {
      this.Obs1.unsubscribe();
    }
  }
  videoUrl: any;

  LoadStreamVideoPromotion() {
    this.CourseService.GetStreamVideoPromotion(this.CourseId).subscribe({
      next: (response: ArrayBuffer) => {
        const videoBlob = new Blob([response], { type: 'video/mp4' });
        const videoUrl = URL.createObjectURL(videoBlob);
        this.videoUrl = this.sanitizer.bypassSecurityTrustUrl(videoUrl);
      },
      error: (err) => {},
    });
  }

  CourseImage: any;
  LoadCourseLandingPage() {
    this.CourseService.GetCourseLandingPage(this.CourseId).subscribe({
      next: (res: any) => {
        var Course = res.data;
        this.landingForm.patchValue({
          Title: Course.title,
          Subtitle: Course.subTitle,
          Decribtion: Course.description,
          Languge: Course.langugeId,
          Category: Course.categoryId,
        });
        this.CourseImage = Course.courseImage;
      },
    });
  }

  CreateLandingForm() {
    this.landingForm = new FormGroup({
      Title: new FormControl('', [Validators.required]),
      Subtitle: new FormControl('', [Validators.required]),
      Decribtion: new FormControl('', [Validators.required]),
      Languge: new FormControl(null, [Validators.required]),
      Category: new FormControl(null, [Validators.required]),
      Image: new FormControl(null),
      PromotionVideo: new FormControl(null),
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

  Obs1: any;
  SaveLandingpageData() {
    this.Obs1 = this.CourseService.GetCourseFireAction().subscribe({
      next: (res) => {
        var data = new MyData();
        data.Data = this.CreateFormDataForSaveCourse();
        data.CourseId = this.CourseId;
        data.isDirty = this.landingForm.dirty;
        data.numberObComponent = ComponentNumbers.landingpageComponentnumber;
        this.CourseService.SetFiredData(data);
      },
    });
  }

  CreateFormDataForSaveCourse(): FormData {
    let courseLandingDTO = new FormData();
    courseLandingDTO.append('CourseId', this.CourseId);
    courseLandingDTO.append('Title', this.landingForm.value.Title);
    courseLandingDTO.append('SubTitle', this.landingForm.value.Subtitle);
    courseLandingDTO.append('Description', this.landingForm.value.Decribtion);
    courseLandingDTO.append('LangugeId', this.landingForm.value.Languge);
    courseLandingDTO.append('CategoryId', this.landingForm.value.Category);

    if (
      this.landingForm.get('Image').value != null ||
      this.landingForm.get('Image').value != undefined
    ) {
      courseLandingDTO.append(
        'Image',
        this.landingForm.value['Image'],
        this.landingForm.value['Image'].name
      );
    }
    if (
      this.landingForm.get('PromotionVideo').value != null ||
      this.landingForm.get('PromotionVideo').value != undefined
    ) {
      courseLandingDTO.append(
        'PromotionVideo',
        this.landingForm.get('PromotionVideo').value,
        this.landingForm.get('PromotionVideo').value.name
      );
    }

    return courseLandingDTO;
  }

  AllowedImageExtension = ['jpg', 'jpeg', 'gif', 'png'];
  SelectImage($event: any, img: HTMLImageElement) {
    const file = $event.target.files[0];
    var extension: string = file.name.split('.')[1];

    if (!this.AllowedImageExtension.includes(extension)) {
      this.Toastr.warning('File not allowed choose valid one');

      return;
    }
    img.parentElement.style.padding = '0';

    img.style.width = '100%';
    img.style.height = '100%';
    if (file) {
      const objectURL = URL.createObjectURL(file);
      img.src = objectURL;

      this.landingForm.get('Image')?.setValue(file);
    }
  }
  AllowedVideoExtension = 'mp4';
  SelectVideo($event: any, video: HTMLVideoElement, img: HTMLImageElement) {
    const file = $event.target.files[0];
    video.src = URL.createObjectURL(file);
    var extension: string = file.type.split('/')[1];

    if (this.AllowedVideoExtension != extension) {
      this.Toastr.warning('File not allowed choose valid one');

      return;
    }
    if (file) {
      this.landingForm.get('PromotionVideo')?.setValue(file);
    }
  }

  Categoris: any;
  GetCategories() {
    this.CourseCategoryService.GetCategories().subscribe({
      next: (res: any) => {
        this.Categoris = res.data;
      },
    });
  }
  languges: any;
  Getlanguges() {
    this.CourseCategoryService.Getlanguges().subscribe({
      next: (res: any) => {
        this.languges = res.data;
      },
    });
  }
}
