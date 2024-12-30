import { TestBed } from '@angular/core/testing';

import { CreatequizService } from './createquiz.service';

describe('CreatequizService', () => {
  let service: CreatequizService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreatequizService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
