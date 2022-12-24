import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { BloodCenter } from '../../model/blood-center.model';
import { BloodCenterService } from '../../services/blood-center.service';
import { AppointmentService } from '../../services/appointment.service';
import { Appointment } from '../../model/appointment.model';
import { Router } from '@angular/router';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Staff } from '../../model/staff.model';
import { StaffService } from '../../services/staff.service';
import { MatLegacyDialog as MatDialog, MatLegacyDialogConfig as MatDialogConfig } from '@angular/material/legacy-dialog';
import { AppointmentDialogComponent } from '../staff-appointment/appointment-dialog.component';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-blood-center-profile',
  templateUrl: './blood-center-profile.component.html',
  styleUrls: ['./blood-center-profile.component.css']
})
export class BloodCenterProfileComponent {

  public center: BloodCenter | undefined;
  public staff: Staff | undefined;
  public allStaff: Staff[] = [];
  public availableAppointments: Appointment[] = [];
  public scheduledAppointments: Appointment[]=[];
  public dataSourceStaff = new MatTableDataSource<Staff>();
  public displayedColumnsStaff = ['name', 'email', 'adress', 'phoneNumber'];
  public dataSourceAppointments = new MatTableDataSource<Appointment>();
  public displayedColumnsAppointments = ['date', 'duration'];


  constructor(private authService: AuthService, private bloodCenterService: BloodCenterService, private staffService: StaffService, private appointmentService: AppointmentService, private dialog: MatDialog, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {

    let id = Number(this.authService.getIdByRole());

    this.staffService.getStaff(id!).subscribe(res => {
      
        this.staff = res;
        this.bloodCenterService.getCenter(res.centerId).subscribe(res1 => {
          this.center = res1;
        });
        this.staffService.getStaffByCenter(res.centerId).subscribe(res1 => {
            this.allStaff = res1.filter(s => s.id != res.id);
            this.dataSourceStaff.data = this.allStaff;
          });
         this.appointmentService.getAvailableByCenter(res.centerId).subscribe(res1 => {
            this.availableAppointments = res1;
            
          });
          this.appointmentService.getScheduledByCenter(res.centerId).subscribe(res1=> {
            this.scheduledAppointments = res1;
            this.availableAppointments.concat(this.scheduledAppointments);
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
        appointment.date = data.dateTime.format('YYYY-MM-DD HH:mm:ss');
        appointment.duration = data.duration;
        this.appointmentService.addAvailable(appointment).subscribe(
          response => {
            console.log(response);
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
