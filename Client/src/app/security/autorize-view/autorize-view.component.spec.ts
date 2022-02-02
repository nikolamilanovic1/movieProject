import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutorizeViewComponent } from './autorize-view.component';

describe('AutorizeViewComponent', () => {
  let component: AutorizeViewComponent;
  let fixture: ComponentFixture<AutorizeViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AutorizeViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AutorizeViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
