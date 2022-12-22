import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorAppointmentScheduleComponent } from './donor-appointment-schedule.component';

describe('DonorAppointmentScheduleComponent', () => {
  let component: DonorAppointmentScheduleComponent;
  let fixture: ComponentFixture<DonorAppointmentScheduleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DonorAppointmentScheduleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorAppointmentScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
