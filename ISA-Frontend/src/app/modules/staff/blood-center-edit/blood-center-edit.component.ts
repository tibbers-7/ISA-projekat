import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, Params } from "@angular/router";
import { BloodCenter } from "../../../model/blood-center.model";
import { Appointment } from "../../../model/appointment.model";
import { AppointmentService } from "../../../services/appointment.service";
import { BloodCenterService } from "../../../services/blood-center.service";
import { User } from "../../../model/user.model";
import { UserService } from "../../../services/user.service";
import { StaffService } from "../../../services/staff.service";
import { AuthService } from "../../../services/auth.service";
import { Staff } from "../../../model/staff.model";

@Component({
  selector: 'app-blood-center-edit',
  templateUrl: './blood-center-edit.component.html',
  styleUrls: ['./blood-center-edit.component.css']
})

export class BloodCenterEditComponent implements OnInit {

  public center: BloodCenter | undefined;
  public staff: Staff | undefined;
 
  constructor(private staffService: StaffService, private authService: AuthService, private bloodCenterService: BloodCenterService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
   
      const id = Number(this.authService.getIdByRole());
      this.staffService.getStaff(id).subscribe(res => {
        this.staff = res;
        this.bloodCenterService.getCenter(this.staff.centerId).subscribe(res1 => {
          this.center = res1;
        });
      });

   

  }

  public updateCenter(): void {
    if (!this.isValidInput()) return;
    this.bloodCenterService.updateCenter(this.center).subscribe(res => {
      this.router.navigate(['staff/center']);
    });
  }

  private isValidInput(): boolean {
    return this.center?.name != '' && this.center?.address != '' && this.center?.description != '';
  }
}
