import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { FormControl, FormGroup } from '@angular/forms';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatSort, Sort } from '@angular/material/sort';
import { Appointment } from '../model/appointment.model';
import { BloodCenter } from '../model/blood-center.model';
import { AppointmentService } from '../services/appointment.service';


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
  public dateTime = new FormControl(new Date(2023,2,2));

  constructor(private appointmentService: AppointmentService) { }

  ngOnInit(): void {
   
  }
 
  applyDateTime() {

    if (this.dateTime.value != null) {

      const chosenDate = this.dateTime.value!.toISOString();
      this.appointmentService.getCentersForDateTime(chosenDate).subscribe(res => {
        this.centers = res.sort((a, b) => b.avgScore - a.avgScore);
        this.dataSource.data = this.centers;
      });

    }
   
  }

  chooseCenter() {
    //check if popunjen upitnik ako ne redirect ili nesto

    //AKO IMA VEC ZAKAZI
    if (this.selectedRow.selected.length != 0) {
      var center = this.selectedRow.selected[0];
      //this.selectedIndex = this.centers.findIndex((d: BloodCenter) => d === center);

      var appointment = new Appointment();


    }

  }
  

}
