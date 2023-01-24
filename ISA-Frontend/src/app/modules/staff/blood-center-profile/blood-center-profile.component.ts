import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { BloodCenter } from '../../../model/blood-center.model';
import { BloodCenterService } from '../../../services/blood-center.service';
import { AppointmentService } from '../../../services/appointment.service';
import { Appointment } from '../../../model/appointment.model';
import { Router } from '@angular/router';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Staff } from '../../../model/staff.model';
import { StaffService } from '../../../services/staff.service';
import { MatLegacyDialog as MatDialog, MatLegacyDialogConfig as MatDialogConfig } from '@angular/material/legacy-dialog';
import { AppointmentDialogComponent } from '../staff-appointment/appointment-dialog.component';
import { AuthService } from '../../../services/auth.service';
import { Address } from 'app/model/address.model';

@Component({
  selector: 'app-blood-center-profile',
  templateUrl: './blood-center-profile.component.html',
  styleUrls: ['./blood-center-profile.component.css']
})
export class BloodCenterProfileComponent {

  public center: BloodCenter | undefined;
  public address: Address | undefined;
  public staff: Staff | undefined;
  public allStaff: Staff[] = [];
  public availableAppointments: Appointment[] = [];
  public dataSourceStaff = new MatTableDataSource<Staff>();
  public displayedColumnsStaff = ['name', 'surname', 'email'];
  public dataSourceAppointments = new MatTableDataSource<Appointment>();
  public displayedColumnsAppointments = ['date', 'duration','staff'];


  constructor(private authService: AuthService, private bloodCenterService: BloodCenterService, private staffService: StaffService, private appointmentService: AppointmentService, private dialog: MatDialog, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
//dobijamo ulogovanog staff id preko local storage
    this.staffService.getStaff(Number(this.authService.getIdByRole())).subscribe(res => {
      //dobijamo staff preko id
      this.staff = res;
      //dobijamo centar preko naseg staffa
      this.bloodCenterService.getCenter(res.centerId).subscribe(res1 => {
          this.center = res1;
          console.log(this.address);
      });
      this.bloodCenterService.getAddressForCenter(res.centerId).subscribe(res1=>{
          this.address = res1;
          console.log(this.address);
      });
      //prikazujemo sve staffove osim ulogovanog iz naseg centra
      this.staffService.getStaffByCenter(res.centerId).subscribe(res1 => {
            this.allStaff = res1.filter(s => s.id != res.id);
            this.dataSourceStaff.data = this.allStaff;
      });

      this.appointmentService.getAvailableByCenter(res.centerId).subscribe(res1 => {
            this.availableAppointments = res1;
            this.dataSourceAppointments.data = this.availableAppointments;
      });
         
        
      });
    }

  editBloodCenter() {
    this.router.navigate(['staff/edit-center']);
  }

  addAppointment() {
    
    const dialogRef = this.dialog.open(AppointmentDialogComponent, { height: '600px', width: '400px' });

    dialogRef.afterClosed().subscribe(
      data => {
        let appointment = new Appointment();
        appointment.staffId = this.staff!.id;
        appointment.centerId = this.center!.id;
        appointment.donorId = 0;
        appointment.date = data.dateTime.format('YYYY-MM-DD HH:mm:ss');
        appointment.duration = data.duration;
        appointment.status = "AVAILABLE";

        this.appointmentService.scheduleStaff(appointment).subscribe(
          response => {
            alert("Uspesno dodavanje")
          },
          error => {
            console.log(error);
            alert("Neuspesno dodavanje");
          }
        )
      }
      
    );
    
  }
}
