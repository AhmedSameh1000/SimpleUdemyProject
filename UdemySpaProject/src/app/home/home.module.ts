import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home/home.component';
import { MaterialModule } from '../material/material.module';
import { CourseFilterComponent } from './course-filter/course-filter.component';
import { FormsModule } from '@angular/forms';
import { CourseDetailsComponent } from './course-details/course-details.component';
import { VideoComponent } from './video/video.component';
import { CartComponent } from './cart/cart.component';
import { PaymentStatusComponent } from './payment-status/payment-status.component';

@NgModule({
  declarations: [HomeComponent, CourseFilterComponent, CourseDetailsComponent, VideoComponent, CartComponent, PaymentStatusComponent],
  imports: [CommonModule, HomeRoutingModule, MaterialModule, FormsModule],
})
export class HomeModule {}
