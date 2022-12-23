import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Appointment } from '../../model/appointment.model';
import { AppointmentService } from '../../services/appointment.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-donor-appointments',
  templateUrl: './donor-appointments.component.html',
  styleUrls: ['./donor-appointments.component.css']
})
export class DonorAppointmentsComponent {

  public dataSource = new MatTableDataSource<Appointment>();
  public appointments:Appointment[]=[];
  public displayedColumns = ['staffId','date','duration'];

  public selectedAppt:Appointment=new Appointment;
  public donorId:number=0;

  constructor(private apptService:AppointmentService, private toast:NgToastService) { }

  ngOnInit(): void {
    this.donorId=Number(localStorage.getItem('idByRole'));
    this.apptService.getScheduledForDonor(this.donorId).subscribe(res => {
      this.appointments=res;
      this.dataSource.data=this.appointments;
    });
  }

  selectAppointment(appt:any){
    this.selectedAppt=appt;
    console.log(appt);
  }
}
