import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, Params } from "@angular/router";
import { BloodCenter } from "../model/blood-center.model";
import { Appointment } from "../model/appointment.model";
import { AppointmentService } from "../services/appointment.service";
import { BloodCenterService } from "../services/blood-center.service";
import { User } from "../model/user.model";
import { UserService } from "../services/user.service";

@Component({
  selector: 'app-blood-center-edit',
  templateUrl: './blood-center-edit.component.html',
  styleUrls: ['./blood-center-edit.component.css']
})

export class BloodCenterEditComponent implements OnInit {

  public center: BloodCenter | undefined;
  public staff: User | undefined;
 
  constructor(private userService: UserService, private bloodCenterService: BloodCenterService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {

      this.userService.getUser(params['id']).subscribe(res => {
        this.staff = res;
      });
      this.bloodCenterService.getCenter(this.staff!.idOfCenter).subscribe(res => {
        this.center = res;
      });
      
    });

  }

  public updateCenter(): void {
    if (!this.isValidInput()) return;
    this.bloodCenterService.updateCenter(this.center).subscribe(res => {
      this.router.navigate(['staff/{id}/center']);
    });
  }

  private isValidInput(): boolean {
    return this.center?.name != '' && this.center?.adress != '' && this.center?.description != '';
  }
}
