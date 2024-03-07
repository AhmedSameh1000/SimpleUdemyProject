import { CartService } from 'src/app/Services/cart.service';
import { AuthService } from './../../Services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-payment-status',
  templateUrl: './payment-status.component.html',
  styleUrls: ['./payment-status.component.css'],
})
export class PaymentStatusComponent implements OnInit {
  constructor(
    private AuthService: AuthService,
    private CartService: CartService
  ) {}
  ngOnInit(): void {
    this.CartService.CoursePaymentConfirmation(
      this.AuthService.GetUserId()
    ).subscribe({
      next: (res) => {
        console.log(res);
      },
    });
  }
}
