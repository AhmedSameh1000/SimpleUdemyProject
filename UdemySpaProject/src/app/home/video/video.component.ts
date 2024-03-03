import { CourseService } from 'src/app/Services/course.service';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-video',
  templateUrl: './video.component.html',
  styleUrls: ['./video.component.css'],
})
export class VideoComponent implements OnInit {
  constructor(
    private CourseService: CourseService,
    @Inject(MAT_DIALOG_DATA) public data: { id: string },
    private sanitizer: DomSanitizer
  ) {}
  ngOnInit(): void {
    console.log(this.data);
    this.LoadStreamVideoPromotion();
  }

  videoUrl: any;

  LoadStreamVideoPromotion() {
    this.CourseService.GetStreamVideoPromotion(this.data).subscribe({
      next: (response: ArrayBuffer) => {
        const videoBlob = new Blob([response], { type: 'video/mp4' });
        const videoUrl = URL.createObjectURL(videoBlob);
        this.videoUrl = this.sanitizer.bypassSecurityTrustUrl(videoUrl);
      },
      error: (err) => {},
    });
  }
}
