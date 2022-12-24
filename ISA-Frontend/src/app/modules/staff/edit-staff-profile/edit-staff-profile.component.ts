import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, Params } from "@angular/router";
import { Staff } from "../../../model/staff.model";
import { User } from "../../../model/user.model";
import { StaffService } from "../../../services/staff.service";
import { UserService } from "../../../services/user.service";

@Component({
  selector: 'app-edit-staff-profile',
  templateUrl: './edit-staff-profile.component.html',
  styleUrls: ['./edit-staff-profile.component.css']
})

export class EditStaffProfileComponent implements OnInit {

  public staff: Staff | undefined;

  constructor(private staffService: StaffService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    
    this.staffService.getStaff(1).subscribe(res => {
      this.staff = res;
    });
   
  }

  public updateStaff(): void {
    if (!this.isValidInput()) return;
    this.staffService.updateStaff(this.staff).subscribe(res => {
      this.router.navigate(['staff/profile']);
    });
  }

  private isValidInput(): boolean {
    return this.staff?.password != '' && this.staff?.name != '' && this.staff?.email != '' && this.staff?.surname != '';
  }
}
