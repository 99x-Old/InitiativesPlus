/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { InitiativesService } from './initiatives.service';

describe('Service: Initiatives', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [InitiativesService]
    });
  });

  it('should ...', inject([InitiativesService], (service: InitiativesService) => {
    expect(service).toBeTruthy();
  }));
});
