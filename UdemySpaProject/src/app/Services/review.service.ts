import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  constructor(private HttpClient: HttpClient) {}

  CreateReview(review: any) {
    return this.HttpClient.post(
      'http://localhost:5227/api/Review/CreateReview',
      review
    );
  }
}
