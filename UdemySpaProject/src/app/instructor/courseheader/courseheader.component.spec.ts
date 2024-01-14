import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseheaderComponent } from './courseheader.component';

describe('CourseheaderComponent', () => {
  let component: CourseheaderComponent;
  let fixture: ComponentFixture<CourseheaderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CourseheaderComponent]
    });
    fixture = TestBed.createComponent(CourseheaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
