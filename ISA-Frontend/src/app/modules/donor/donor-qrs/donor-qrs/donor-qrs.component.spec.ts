import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorQrsComponent } from './donor-qrs.component';

describe('DonorQrsComponent', () => {
  let component: DonorQrsComponent;
  let fixture: ComponentFixture<DonorQrsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DonorQrsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorQrsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
