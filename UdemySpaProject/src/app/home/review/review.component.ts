import { ReviewService } from './../../Services/review.service';
import { AuthService } from './../../Services/auth.service';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css'],
})
export class ReviewComponent implements OnInit {
  Rate: any = 0;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { id: any },
    private AuthService: AuthService,
    private ReviewService: ReviewService,
    private MatDialogRef: MatDialogRef<ReviewComponent>
  ) {}
  ngOnInit(): void {
    console.log(this.data.id);
    this.initializeReviewForm();
  }
  ReviewForm: FormGroup;

  initializeReviewForm() {
    this.ReviewForm = new FormGroup({
      userId: new FormControl(this.AuthService.GetUserId()),
      courseId: new FormControl(+this.data.id),
      text: new FormControl(''),
      stars: new FormControl(0),
    });
  }

  SaveReview() {
    this.ReviewService.CreateReview(this.ReviewForm.value).subscribe({
      next: (res) => {
        this.MatDialogRef.close(true);
      },
    });
  }
}
