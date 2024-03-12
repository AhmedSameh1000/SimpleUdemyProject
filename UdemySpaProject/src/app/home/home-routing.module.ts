import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CourseFilterComponent } from './course-filter/course-filter.component';
import { CourseDetailsComponent } from './course-details/course-details.component';
import { CartComponent } from './cart/cart.component';
import { PaymentStatusComponent } from './payment-status/payment-status.component';
import { CourseStartComponent } from './course-start/course-start.component';
import { LearningComponent } from './learning/learning.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'filterCourses',
    component: CourseFilterComponent,
  },
  {
    path: 'courseDetails/:Id',
    component: CourseDetailsComponent,
  },
  {
    path: 'cart',
    component: CartComponent,
  },
  {
    path: 'paymentstatus',
    component: PaymentStatusComponent,
  },
  {
    path: 'coursestart/:Id',
    component: CourseStartComponent,
  },
  {
    path: 'learning',
    component: LearningComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeRoutingModule {}
