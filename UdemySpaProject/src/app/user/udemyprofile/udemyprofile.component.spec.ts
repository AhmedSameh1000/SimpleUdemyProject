import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UdemyprofileComponent } from './udemyprofile.component';

describe('UdemyprofileComponent', () => {
  let component: UdemyprofileComponent;
  let fixture: ComponentFixture<UdemyprofileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UdemyprofileComponent]
    });
    fixture = TestBed.createComponent(UdemyprofileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
