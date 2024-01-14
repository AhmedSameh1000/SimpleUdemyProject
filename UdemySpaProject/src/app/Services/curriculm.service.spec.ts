import { TestBed } from '@angular/core/testing';

import { CurriculmService } from './curriculm.service';

describe('CurriculmService', () => {
  let service: CurriculmService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CurriculmService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
