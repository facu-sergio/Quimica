import { TestBed } from '@angular/core/testing';

import { ProductsSummaryService } from './products-summary.service';

describe('ProductsSummaryService', () => {
  let service: ProductsSummaryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductsSummaryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
