import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateMovieTeaterComponent } from './create-movie-teater.component';

describe('CreateMovieTeaterComponent', () => {
  let component: CreateMovieTeaterComponent;
  let fixture: ComponentFixture<CreateMovieTeaterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateMovieTeaterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateMovieTeaterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
