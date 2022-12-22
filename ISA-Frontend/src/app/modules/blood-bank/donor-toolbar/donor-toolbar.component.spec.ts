import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorToolbarComponent } from './donor-toolbar.component';

describe('DonorToolbarComponent', () => {
  let component: DonorToolbarComponent;
  let fixture: ComponentFixture<DonorToolbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DonorToolbarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
