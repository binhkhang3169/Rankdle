import { TestBed } from '@angular/core/testing';

import { ItemSelectServiceService } from './item-select-service.service';

describe('ItemSelectServiceService', () => {
  let service: ItemSelectServiceService;
  

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ItemSelectServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
