import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { BloodCenter } from '../model/blood-center.model';
import { BloodCenterService } from '../services/blood-center.service';
import { User } from '../model/user.model';
import { UserService } from '../services/user.service';
import { AppointmentService } from '../services/appointment.service';
import { Appointment } from '../model/appointment.model';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-blood-center-profile',
  templateUrl: './blood-center-profile.component.html',
  styleUrls: ['./blood-center-profile.component.css']
})
export class BloodCenterProfileComponent {

  public center: BloodCenter | undefined;
  public staff: User | undefined;
  public allStaff: User[] = [];
  public allAppointments: Appointment[] = [];
  public dataSourceStaff = new MatTableDataSource<User>();
  public displayedColumnsStaff = ['name', 'email', 'adress', 'phoneNumber'];
  public dataSourceAppointments = new MatTableDataSource<Appointment>();
  public displayedColumnsAppointments = ['date', 'duration'];


  constructor(private bloodCenterService: BloodCenterService, private userService: UserService, private appointmentService: AppointmentService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.userService.getUser(params['id']).subscribe(res => {
        this.staff = res;
        this.bloodCenterService.getCenter(res.idOfCenter).subscribe(res1 => {
          this.center = res1;
          this.userService.getStaffByCenter(res1.id).subscribe(res2 => {
            this.allStaff = res2;
            this.dataSourceStaff.data = this.allStaff;
          });
          this.appointmentService.getByCenter(res1.id).subscribe(res3 => {
            this.allAppointments = res3;
            this.dataSourceAppointments.data = this.allAppointments;
          });
        });
      });
     
    });

  }
  editBloodCenter() {
    this.router.navigate(['staff/{id}/edit-center', { id: this.staff?.id }]);
  }
}
