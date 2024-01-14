import { TestBed } from '@angular/core/testing';

import { CourseLectureService } from './course-lecture.service';

describe('CourseLectureService', () => {
  let service: CourseLectureService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CourseLectureService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
