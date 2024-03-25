import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CourseFilterComponent } from './course-filter/course-filter.component';
import { CourseDetailsComponent } from './course-details/course-details.component';
import { CartComponent } from './cart/cart.component';
import { PaymentStatusComponent } from './payment-status/payment-status.component';
import { CourseStartComponent } from './course-start/course-start.component';
import { LearningComponent } from './learning/learning.component';
import { CanActivate } from '../Guards/AuthGuard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    canActivate: [CanActivate],
    path: 'filterCourses',
    component: CourseFilterComponent,
  },
  {
    canActivate: [CanActivate],
    path: 'courseDetails/:Id',
    component: CourseDetailsComponent,
  },
  {
    canActivate: [CanActivate],
    path: 'cart',
    component: CartComponent,
  },
  {
    canActivate: [CanActivate],
    path: 'paymentstatus',
    component: PaymentStatusComponent,
  },
  {
    canActivate: [CanActivate],

    path: 'coursestart/:Id',
    component: CourseStartComponent,
  },
  {
    canActivate: [CanActivate],
    path: 'learning',
    component: LearningComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeRoutingModule {}
