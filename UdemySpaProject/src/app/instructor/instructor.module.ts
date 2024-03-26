import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InstructorRoutingModule } from './instructor-routing.module';
import { LandingComponent } from './landing/landing.component';
import { MaterialModule } from '../material/material.module';
import { BasicDataComponent } from './basic-data/basic-data.component';
import { RouterModule } from '@angular/router';
import { CoursecreationComponent } from './coursecreation/coursecreation.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CourseplaningComponent } from './courseplaning/courseplaning.component';
import { CourseheaderComponent } from './courseheader/courseheader.component';
import { CourserequirmentComponent } from './courserequirment/courserequirment.component';
import { CurriculmComponent } from './curriculm/curriculm.component';
import { LandingpageComponent } from './landingpage/landingpage.component';
import { PricingComponent } from './pricing/pricing.component';
import { CourseMessageComponent } from './course-message/course-message.component';
import { CanSubmitComponent } from './can-submit/can-submit.component';

@NgModule({
  declarations: [
    LandingComponent,
    BasicDataComponent,
    CoursecreationComponent,
    CourseplaningComponent,
    CourseheaderComponent,
    CourserequirmentComponent,
    CurriculmComponent,
    LandingpageComponent,
    PricingComponent,
    CourseMessageComponent,
    CanSubmitComponent,
  ],
  imports: [
    CommonModule,
    InstructorRoutingModule,
    MaterialModule,
    RouterModule,
    ReactiveFormsModule,
  ],
})
export class InstructorModule {}
