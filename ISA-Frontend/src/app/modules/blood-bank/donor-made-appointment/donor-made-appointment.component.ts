import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { FormControl, FormGroup } from '@angular/forms';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatSort, Sort } from '@angular/material/sort';
import * as moment from 'moment';
import { Appointment } from '../model/appointment.model';
import { BloodCenter } from '../model/blood-center.model';
import { Donor } from '../model/donor.model';
import { AppointmentService } from '../services/appointment.service';
import { AuthService } from '../services/auth.service';
import { DonorService } from '../services/donor.service';
import { FormService } from '../services/form.service';


@Component({
  selector: 'app-donor-made-appointment',
  templateUrl: './donor-made-appointment.component.html',
  styleUrls: ['./donor-made-appointment.component.css']
})
export class DonorMadeAppointmentComponent implements OnInit {

  public centers: BloodCenter[] = [];
  public dataSource = new MatTableDataSource<BloodCenter>();
  public selectedRow = new SelectionModel<BloodCenter>(false, []);
  public selectedIndex = 0;
  public displayedColumns = ['name', 'adress', 'avgScore'];
  public minDate = new Date();
  public dateTime = new FormControl(moment(new Date()));
  public chosenDate = '';
  public showForm: boolean = false;
  public donor!: Donor;
  public center!: BloodCenter;

  constructor(private appointmentService: AppointmentService, private donorService:DonorService, private authService: AuthService, private formService: FormService) { }

  ngOnInit(): void {
    let id = Number(this.authService.getIdByRole());
    this.donorService.getDonor(id).subscribe(res => {
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
    this.formService.isEligible(this.donor?.id).subscribe(response => {
      //popunjen je
      if (this.selectedRow.selected.length != 0) {
        this.center = this.selectedRow.selected[0];
        this.makeAppointment();
      }
    },
      error => {

        this.showForm = true;
      });

  //neka provera da li je popunjena?
    if (this.selectedRow.selected.length != 0) {
      this.center = this.selectedRow.selected[0];
      this.makeAppointment();
    }
  }

  makeAppointment() {
  var appointment = new Appointment();
  appointment.date = this.chosenDate;
  appointment.centerId = this.center?.id;
  appointment.donorId = this.donor?.id;
    appointment.duration = 30;
    this.appointmentService.makeDonorAppointment(appointment).subscribe(res => {

      console.log("uspelo je ")
    },
error => console.log("greska")    )
  
  }

}
