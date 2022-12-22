import { Component, OnInit } from '@angular/core';
import {MatTableDataSource} from '@angular/material/table'
import { Appointment } from '../../model/appointment.model';

@Component({
  selector: 'app-donor-appointment-schedule',
  templateUrl: './donor-appointment-schedule.component.html',
  styleUrls: ['./donor-appointment-schedule.component.css']
})
export class DonorAppointmentScheduleComponent implements OnInit {

  public dataSource = new MatTableDataSource<Appointment>();
  public cities: string[]=[];
  public displayedColumns = ['staffId','centerId','date','duration'];
  constructor() { }

  ngOnInit(): void {
  }

}
