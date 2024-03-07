import { AuthService } from 'src/app/Services/auth.service';
import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/Services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  rate = 4;
  constructor(
    private AuthService: AuthService,
    private CartServices: CartService
  ) {}
  ngOnInit(): void {
    this.LoadCart();
  }

  Cart: any;
  LoadCart() {
    this.CartServices.GetCartByUserid(this.AuthService.GetUserId()).subscribe({
      next: (res: any) => {
        console.log(res);
        this.Cart = res.data;
      },
    });
  }
  RemoveFromCart(courseId) {
    this.CartServices.RemoveCartItem(
      courseId,
      this.AuthService.GetUserId()
    ).subscribe({
      next: (res) => {
        this.LoadCart();
      },
    });
  }
  CheckOut() {
    var checkOutProperties = {
      url:
        window.location.protocol +
        '//' +
        window.location.host +
        '/paymentstatus',
      cartId: this.Cart.cartId,
      userId: this.AuthService.GetUserId(),
    };
    this.CartServices.CheckOut(checkOutProperties).subscribe({
      next: (res: any) => {
        console.log(res);
        window.location.href = res.data.url;
      },
    });
  }
}
