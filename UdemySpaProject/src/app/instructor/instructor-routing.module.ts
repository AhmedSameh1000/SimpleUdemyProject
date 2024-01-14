import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './landing/landing.component';
import { BasicDataComponent } from './basic-data/basic-data.component';
import { CoursecreationComponent } from './coursecreation/coursecreation.component';

const routes: Routes = [
  {
    path: 'landing',
    component: LandingComponent,
  },
  {
    path: 'basic',
    component: BasicDataComponent,
  },
  {
    path: 'coursecreation/:Id',
    component: CoursecreationComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InstructorRoutingModule {}
