import {
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ComponentNumbers } from 'src/app/Models/component-numbers';
import { CourseLectureService } from 'src/app/Services/course-lecture.service';
import { CourseSectionService } from 'src/app/Services/course-section.service';
import { CurriculmService } from 'src/app/Services/curriculm.service';
const URL = 'https://evening-anchorage-3159.herokuapp.com/api/';

@Component({
  selector: 'app-curriculm',
  templateUrl: './curriculm.component.html',
  styleUrls: ['./curriculm.component.css'],
})
export class CurriculmComponent implements OnInit, OnDestroy {
  CurriculmForm: FormGroup;
  panelOpenState = false;

  constructor(
    private Service: CurriculmService,
    private SectionService: CourseSectionService,
    private ActivatedRoute: ActivatedRoute,
    private LectureService: CourseLectureService
  ) {}

  ngOnInit(): void {
    this.GetCoursId();
    this.Service.ISCurriculmComponent(true);

    localStorage.setItem(
      'SelectedComponent',
      ComponentNumbers.curriculmComponentnumber.toString()
    );
    this.CreateCurriculmForm();
    this.LoadSections();
  }

  LoadSections() {
    this.SectionService.GetSections(this.CourseId).subscribe({
      next: (res: any) => {
        this.SetSections(res.data);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  SaveSection(sectionId, sectiontitle, SectionDescription) {
    var forUpdateDTO = {
      CourseId: this.CourseId,
      SectionId: sectionId,
      SectionTitle: sectiontitle,
      SectionDescription: SectionDescription,
    };
    this.SectionService.UpdateSection(forUpdateDTO).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
  SetSections(Sectionsdata: any) {
    const Sections = this.CurriculmForm.get('Sections') as FormArray;

    console.log(Sectionsdata);
    Sectionsdata.forEach((sectionData: any) => {
      const group = new FormGroup({
        id: new FormControl(sectionData.id),
        SectionTitle: new FormControl(sectionData.title),
        SectionDescription: new FormControl(
          sectionData.whatStudentLearnFromthisSection
        ),
        Lectures: new FormArray([]),
      });

      const lecturesArray = group.get('Lectures') as FormArray;

      (sectionData.lectures || []).forEach((lecture: any) => {
        const lectureGroup = new FormGroup({
          id: new FormControl(lecture.id),
          Lecturetitle: new FormControl(lecture.title),
          LectureDescription: new FormControl(lecture.description),
          videoSectionUrl: new FormControl(lecture.videoSectionUrl),
          menutes: new FormControl(lecture.menutes),
        });

        lecturesArray.push(lectureGroup);
      });

      Sections.push(group);
    });
  }
  DeletedSection(id: any) {
    console.log(id);
  }
  DeleteLecture(id: any) {
    console.log(id);
  }
  CreateCurriculmForm() {
    this.CurriculmForm = new FormGroup({
      Sections: new FormArray([]),
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
  AddSection() {
    this.SectionService.CreateSection(this.CourseId).subscribe({
      next: (res) => {
        var Sections = this.CurriculmForm.get('Sections') as FormArray;
        var Group = new FormGroup({
          SectionTitle: new FormControl(),
          SectionDescription: new FormControl(),
          Lectures: new FormArray([]),
        });
        Sections.push(Group);
      },
    });
  }

  AddLecture(sectionIndex: number, SectionId: any) {
    this.LectureService.CreateLecture(SectionId).subscribe({
      next: (res) => {
        var Sections = this.CurriculmForm.get('Sections') as FormArray;
        var Lectures = Sections.at(sectionIndex).get('Lectures') as FormArray;

        var Lecture = new FormGroup({
          id: new FormControl(),
          Lecturetitle: new FormControl(),
          LectureDescription: new FormControl(),
          videoSectionUrl: new FormControl(),
          menutes: new FormControl(),
        });

        Lectures.push(Lecture);
      },
    });
  }

  Show(div: HTMLDivElement) {
    div.classList.toggle('d-none');
  }
  ngOnDestroy(): void {
    this.Service.ISCurriculmComponent(false);
  }
}
