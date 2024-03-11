import { ActivatedRoute } from '@angular/router';
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
    private CartService: CartService,
    private ActivatedRoute: ActivatedRoute
  ) {}
  ngOnInit(): void {
    this.GetPaymentStatus();
    this.ConfirmCourse();
  }
  ConfirmCourse() {
    this.CartService.CoursePaymentConfirmation(
      this.AuthService.GetUserId()
    ).subscribe({
      next: (res) => {},
    });
  }

  PaymentStatus: any;
  GetPaymentStatus() {
    this.ActivatedRoute.queryParamMap.subscribe({
      next: (res) => {
        this.PaymentStatus =
          res.get('paymentsuccessfuly') == 'True' ? true : false;
      },
    });
  }
}
