import { Component, OnInit } from '@angular/core';
import {MatTableDataSource} from '@angular/material/table'
import { Appointment } from '../../model/appointment.model';
import { BloodCenter } from '../../model/blood-center.model';
import { BloodCenterService } from '../../services/blood-center.service';
import { AppointmentService } from '../../services/appointment.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-donor-appointment-schedule',
  templateUrl: './donor-appointment-schedule.component.html',
  styleUrls: ['./donor-appointment-schedule.component.css']
})
export class DonorAppointmentScheduleComponent implements OnInit {

  public dataSource = new MatTableDataSource<Appointment>();
  public appointments:Appointment[]=[];
  public displayedColumns = ['staffId','date','duration'];
  public tableShow:boolean=false;

  public centers:BloodCenter[]=[];
  public centerId:string='';

  public selectedAppt:Appointment=new Appointment;
  private idNum:number=0;
  constructor(private centerService:BloodCenterService,private apptService:AppointmentService, private toast:NgToastService) { }

  ngOnInit(): void {
    this.centerService.getCenters().subscribe(res => {
      this.centers = res;
    });
  }

  getCenters(){
    if (this.centerId!=''){
      this.idNum=Number(this.centerId);
      this.apptService.getAvailableByCenter(this.idNum).subscribe(res => {
        this.appointments = res;
        this.dataSource.data = this.appointments;
      });

      this.tableShow=true;
    }
  }

  selectAppointment(appt:any){
    this.selectedAppt=appt;
    console.log(appt);
  }

  schedule(){
    this.selectedAppt.donorId=Number(localStorage.getItem('idByRole'));
    this.apptService.scheduleAppt(this.selectedAppt).subscribe(res => {

      this.toast.success({detail:"Appointment scheduled!",summary:'',duration:3000});

    }, error=>{
      this.toast.error({detail:'Something went wrong!',summary:"",duration:3000});

    });
  }

}
