import { TestBed } from '@angular/core/testing';

import { MovieTeatersService } from './movie-teaters.service';

describe('MovieTeatersService', () => {
  let service: MovieTeatersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MovieTeatersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
