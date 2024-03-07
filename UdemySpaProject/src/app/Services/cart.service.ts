import { HttpClient, HttpHeaders } from '@angular/common/http';
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
  CheckOut(checkOutProperties: any) {
    return this.HttpClient.post(
      `http://localhost:5227/api/Cart/CheckOut`,
      checkOutProperties
    );
  }

  GetCartByUserid(userId: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/Cart?userId=${userId}`
    );
  }
  CoursePaymentConfirmation(userId: any) {
    return this.HttpClient.get(
      `http://localhost:5227/api/Cart/CoursePaymentConfirmation?userId=${userId}`
    );
  }

  RemoveCartItem(cartItemId, userId: any) {
    return this.HttpClient.delete(
      `http://localhost:5227/api/CartItem/RemoveCartItem?CartItemId=${cartItemId}&userId=${userId}`
    );
  }
}
