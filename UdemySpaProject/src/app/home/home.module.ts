import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home/home.component';
import { MaterialModule } from '../material/material.module';
import { CourseFilterComponent } from './course-filter/course-filter.component';
import { FormsModule } from '@angular/forms';
import { CourseDetailsComponent } from './course-details/course-details.component';

@NgModule({
  declarations: [HomeComponent, CourseFilterComponent, CourseDetailsComponent],
  imports: [CommonModule, HomeRoutingModule, MaterialModule, FormsModule],
})
export class HomeModule {}
