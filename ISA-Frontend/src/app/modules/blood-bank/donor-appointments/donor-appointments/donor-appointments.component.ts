import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Appointment } from '../../model/appointment.model';
import { AppointmentService } from '../../services/appointment.service';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from '../../services/auth.service';

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
  public donorId:any;

  constructor(private apptService:AppointmentService, private toast:NgToastService,private authService:AuthService) { }

  ngOnInit(): void {
    console.log(localStorage.getItem('idByRole'));
    this.donorId=this.authService.getIdByRole;
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
