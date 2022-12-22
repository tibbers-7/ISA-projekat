import { Component, OnInit } from '@angular/core';
import {MatTableDataSource} from '@angular/material/table'
import { Appointment } from '../../model/appointment.model';
import { BloodCenter } from '../../model/blood-center.model';
import { BloodCenterService } from '../../services/blood-center.service';
import { AppointmentService } from '../../services/appointment.service';

@Component({
  selector: 'app-donor-appointment-schedule',
  templateUrl: './donor-appointment-schedule.component.html',
  styleUrls: ['./donor-appointment-schedule.component.css']
})
export class DonorAppointmentScheduleComponent implements OnInit {

  public dataSource = new MatTableDataSource<Appointment>();
  public cities: string[]=[];
  public appointments:Appointment[]=[];
  public displayedColumns = ['staffId','date','duration'];
  public tableShow:boolean=false;

  public centers:BloodCenter[]=[];
  public centerId:string='';

  private idNum:number=0;
  constructor(private centerService:BloodCenterService,private apptService:AppointmentService) { }

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

}
