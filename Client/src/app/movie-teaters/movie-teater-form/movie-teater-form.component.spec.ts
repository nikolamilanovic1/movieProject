import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieTeaterFormComponent } from './movie-teater-form.component';

describe('MovieTeaterFormComponent', () => {
  let component: MovieTeaterFormComponent;
  let fixture: ComponentFixture<MovieTeaterFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MovieTeaterFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MovieTeaterFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
