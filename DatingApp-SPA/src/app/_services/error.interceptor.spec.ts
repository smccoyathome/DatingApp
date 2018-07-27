/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { Error.interceptorService } from './error.interceptor';

describe('Service: Error.interceptor', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Error.interceptorService]
    });
  });

  it('should ...', inject([Error.interceptorService], (service: Error.interceptorService) => {
    expect(service).toBeTruthy();
  }));
});
