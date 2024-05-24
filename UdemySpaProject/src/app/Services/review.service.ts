import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  constructor(private HttpClient: HttpClient) {}

  CreateReview(review: any) {
    return this.HttpClient.post(
      environment.BaseUrl + 'Review/CreateReview',
      review
    );
  }
}
