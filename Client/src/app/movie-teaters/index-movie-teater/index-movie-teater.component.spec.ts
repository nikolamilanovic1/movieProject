import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndexMovieTeaterComponent } from './index-movie-teater.component';

describe('IndexMovieTeaterComponent', () => {
  let component: IndexMovieTeaterComponent;
  let fixture: ComponentFixture<IndexMovieTeaterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndexMovieTeaterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IndexMovieTeaterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
