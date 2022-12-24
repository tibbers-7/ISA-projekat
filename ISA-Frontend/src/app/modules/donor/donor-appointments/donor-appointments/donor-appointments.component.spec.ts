import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorAppointmentsComponent } from './donor-appointments.component';

describe('DonorAppointmentsComponent', () => {
  let component: DonorAppointmentsComponent;
  let fixture: ComponentFixture<DonorAppointmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DonorAppointmentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorAppointmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
