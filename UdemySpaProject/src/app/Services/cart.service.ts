import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(private HttpClient: HttpClient) {}

  AddToCart(CourseToAdd) {
    return this.HttpClient.post(
      'http://localhost:5227/api/CartItem',
      CourseToAdd
    );
  }
}
