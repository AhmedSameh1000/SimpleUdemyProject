import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountsecurityComponent } from './accountsecurity.component';

describe('AccountsecurityComponent', () => {
  let component: AccountsecurityComponent;
  let fixture: ComponentFixture<AccountsecurityComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccountsecurityComponent]
    });
    fixture = TestBed.createComponent(AccountsecurityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
