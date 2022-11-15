import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { BloodCenter } from '../model/blood-center.model';
import { BloodCenterService } from '../services/blood-center.service';
import { User } from '../model/user.model';
import { UserService } from '../services/user.service';
import { AppointmentService } from '../services/appointment.service';
import { Appointment } from '../model/appointment.model';
import { Router } from '@angular/router';

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

  constructor(private bloodCenterService: BloodCenterService, private userService: UserService, private appointmentService: AppointmentService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {

      this.userService.getUser(params['id']).subscribe(res => {
        this.staff = res;
      });
      this.bloodCenterService.getCenter(this.staff!.idOfCenter).subscribe(res => {
        this.center = res;
      });
      this.userService.getStaffByCenter(this.center!.id).subscribe(res => {
        this.allStaff = res;
        this.center!.staff = this.allStaff;
      });
      this.appointmentService.getByCenter(this.center!.id).subscribe(res => {
        this.allAppointments = res;
        this.center!.appointments = this.allAppointments;
      });
    });

  }
  editBloodCenter() {
    this.router.navigate(['staff/{id}/edit-center', { id: this.route.snapshot.paramMap.get('id') }]);
  }
}
