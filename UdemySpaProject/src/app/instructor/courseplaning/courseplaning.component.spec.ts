import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseplaningComponent } from './courseplaning.component';

describe('CourseplaningComponent', () => {
  let component: CourseplaningComponent;
  let fixture: ComponentFixture<CourseplaningComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CourseplaningComponent]
    });
    fixture = TestBed.createComponent(CourseplaningComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
