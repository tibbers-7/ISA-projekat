import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartToolbarComponent } from './start-toolbar.component';

describe('StartToolbarComponent', () => {
  let component: StartToolbarComponent;
  let fixture: ComponentFixture<StartToolbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StartToolbarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StartToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
