import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { FormControl, FormGroup } from '@angular/forms';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatSort, Sort } from '@angular/material/sort';
import * as moment from 'moment';
import { NgToastService } from 'ng-angular-popup';
import { Appointment } from '../model/appointment.model';
import { BloodCenter } from '../model/blood-center.model';
import { Donor } from '../model/donor.model';
import { AppointmentService } from '../services/appointment.service';
import { AuthService } from '../services/auth.service';
import { DonorService } from '../services/donor.service';
import { FormService } from '../services/form.service';
import { StaffService } from '../services/staff.service';


@Component({
  selector: 'app-donor-made-appointment',
  templateUrl: './donor-made-appointment.component.html',
  styleUrls: ['./donor-made-appointment.component.css']
})
export class DonorMadeAppointmentComponent implements OnInit {

  public centers: BloodCenter[] = [];
  public dataSource = new MatTableDataSource<BloodCenter>();
  public selectedCenter: BloodCenter = new BloodCenter;
  public selectedIndex = 0;
  public displayedColumns = ['name', 'adress', 'avgScore'];
  public minDate = new Date();
  public dateTime = new FormControl(moment(new Date()));
  public chosenDate = '';
  public showForm: boolean = false;
  public donorId: number = 0;
  public staffId: number = 0;
  public donor!: Donor;
  public center!: BloodCenter;

  constructor(private appointmentService: AppointmentService, private donorService: DonorService, private staffService: StaffService,
    private authService: AuthService, private formService: FormService, private toast: NgToastService) { }

  ngOnInit(): void {
    this.donorId = Number(this.authService.getIdByRole());
    console.log(this.donorId);
    this.donorService.getDonor(this.donorId).subscribe(res => {
      this.donor = res;
    })
  }
 
  applyDateTime() {

    if (this.dateTime.value != null) {
     this.chosenDate = this.dateTime.value.format('YYYY-MM-DD HH:mm:ss');
      this.appointmentService.getCentersForDateTime(this.chosenDate).subscribe(res => {
        this.centers = res.sort((a, b) => b.avgScore - a.avgScore);
        this.dataSource.data = this.centers;
      });

    }
   
  }

  chooseCenter() {
    //check if popunjen upitnik ako ne redirect ili nesto
    this.formService.isEligible(this.donorId).subscribe(response => {
      //popunjen je
     
        this.makeAppointment();
        this.toast.success({detail:"Appointment scheduled!",summary:'',duration:3000});    
    },
      error => {
        this.toast.error({detail:'You haven\'t filled the form or you already gave blood recently!',summary:"",duration:3000});
      });
  }

  selectCenter(cntr:any){
    this.selectedCenter=cntr;
  }

  makeAppointment() {
    
    let appointment = new Appointment();  
    appointment.date = this.chosenDate;
    appointment.centerId = this.selectedCenter.id;
    appointment.donorId = this.donorId;
    //hardkodovano pozvacu staff servis da nadje random by centerId i onda uzmem prvog
    this.staffService.getStaffByCenter(this.selectedCenter.id).subscribe(res => {
      this.staffId = res.at(0)!.id;
    });
    appointment.staffId = this.staffId;
    appointment.duration = 30;
    appointment.status = "SCHEDULED";
    this.appointmentService.newAppointment(appointment).subscribe(res => {
      console.log("uspelo je ")
    },
    error => console.log("greska"))
  }

}
