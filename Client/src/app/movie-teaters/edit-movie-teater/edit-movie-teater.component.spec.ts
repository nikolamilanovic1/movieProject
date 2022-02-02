import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditMovieTeaterComponent } from './edit-movie-teater.component';

describe('EditMovieTeaterComponent', () => {
  let component: EditMovieTeaterComponent;
  let fixture: ComponentFixture<EditMovieTeaterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditMovieTeaterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditMovieTeaterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
