import { ToastrService } from 'ngx-toastr';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ComponentNumbers } from 'src/app/Models/component-numbers';
import { CourseLectureService } from 'src/app/Services/course-lecture.service';
import { CourseSectionService } from 'src/app/Services/course-section.service';
import { CurriculmService } from 'src/app/Services/curriculm.service';
import Swal from 'sweetalert2';
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
    private LectureService: CourseLectureService,
    private ToastrService: ToastrService
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
      error: (err) => {},
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
      next: (res) => {},
      error: (err) => {},
    });
  }
  SetSections(Sectionsdata: any) {
    const Sections = this.CurriculmForm.get('Sections') as FormArray;

    Sections.clear();
    Sectionsdata.forEach((sectionData: any) => {
      const group = new FormGroup({
        id: new FormControl(sectionData.id),
        SectionTitle: new FormControl(sectionData.title ?? ''),
        SectionDescription: new FormControl(
          sectionData.whatStudentLearnFromthisSection ?? ''
        ),
        Lectures: new FormArray([]),
      });

      const lecturesArray = group.get('Lectures') as FormArray;

      (sectionData.lectures || []).forEach((lecture: any) => {
        const lectureGroup = new FormGroup({
          id: new FormControl(lecture.id),
          Lecturetitle: new FormControl(lecture.title ?? ''),
          LectureDescription: new FormControl(lecture.description ?? ''),
          videoSectionUrl: new FormControl(lecture.videoSectionUrl ?? ''),
          menutes: new FormControl(lecture.menutes ?? ''),
        });

        lecturesArray.push(lectureGroup);
      });

      Sections.push(group);
    });
  }
  DeletedSection(id: any, Index: any) {
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
        this.SectionService.DeleteSection(id).subscribe({
          next: (res) => {
            var Sections = this.CurriculmForm.get('Sections') as FormArray;
            Sections.removeAt(Index);
          },
        });
      }
    });
  }
  DeleteLecture(id: any, sectionIndex, index: any) {
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
        this.LectureService.DeleteLecture(id).subscribe({
          next: (res) => {
            var Sections = this.CurriculmForm.get('Sections') as FormArray;
            var Lectures = Sections.at(sectionIndex).get(
              'Lectures'
            ) as FormArray;
            Lectures.removeAt(index);
          },
        });
      }
    });
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
        this.LoadSections();
      },
    });
  }

  AddLecture(SectionId: any) {
    this.LectureService.CreateLecture(SectionId).subscribe({
      next: (res) => {
        this.LoadSections();
      },
    });
  }
  File: File;
  AllowedVideoExtension = 'mp4';
  SelectFile(event: any) {
    this.File = event.target.files[0];
  }

  SaveLecture(id, title, description) {
    if (this.File != null || this.File != undefined) {
      var extension: string = this.File.type.split('/')[1];
      if (this.AllowedVideoExtension != extension) {
        this.ToastrService.warning(
          `File not allowed choose valid one with Extension ${this.AllowedVideoExtension}`
        );
        return;
      }
    }
    let CourseLecture = new FormData();
    CourseLecture.append('id', id);
    CourseLecture.append('Title', title);
    CourseLecture.append('Description', description);

    if (this.File != null || this.File != undefined) {
      CourseLecture.append('Video', this.File, this.File.name);
    }
    this.LectureService.UpdateLecture(CourseLecture).subscribe({
      next: (res) => {
        this.LoadSections();
        console.log(res);
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
