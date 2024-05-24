import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(private HttpClient: HttpClient) {}

  AddToCart(CourseToAdd) {
    return this.HttpClient.post(environment.BaseUrl + 'CartItem', CourseToAdd);
  }
  CheckOut(checkOutProperties: any) {
    return this.HttpClient.post(
      environment.BaseUrl + `Cart/CheckOut`,
      checkOutProperties
    );
  }

  GetCartByUserid(userId: any) {
    return this.HttpClient.get(environment.BaseUrl + `Cart?userId=${userId}`);
  }
  CoursePaymentConfirmation(userId: any) {
    return this.HttpClient.get(
      environment.BaseUrl + `Cart/CoursePaymentConfirmation?userId=${userId}`
    );
  }

  RemoveCartItem(cartItemId, userId: any) {
    return this.HttpClient.delete(
      environment.BaseUrl +
        `CartItem/RemoveCartItem?CartItemId=${cartItemId}&userId=${userId}`
    );
  }
}
