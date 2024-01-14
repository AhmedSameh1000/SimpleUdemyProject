import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourserequirmentComponent } from './courserequirment.component';

describe('CourserequirmentComponent', () => {
  let component: CourserequirmentComponent;
  let fixture: ComponentFixture<CourserequirmentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CourserequirmentComponent]
    });
    fixture = TestBed.createComponent(CourserequirmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
