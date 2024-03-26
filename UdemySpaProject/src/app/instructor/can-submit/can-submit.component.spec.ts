import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CanSubmitComponent } from './can-submit.component';

describe('CanSubmitComponent', () => {
  let component: CanSubmitComponent;
  let fixture: ComponentFixture<CanSubmitComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CanSubmitComponent]
    });
    fixture = TestBed.createComponent(CanSubmitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
