import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params} from '@angular/router';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { Staff } from '../model/staff.model';
import { StaffService } from '../services/staff.service';

@Component({
  selector: 'app-staff-profile',
  templateUrl: './staff-profile.component.html',
  styleUrls: ['./staff-profile.component.css']
})
export class StaffProfileComponent {

  public staff: Staff | undefined;

  constructor(private staffService: StaffService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    //promeniti kad dodje localstorage
    this.staffService.getStaff(1).subscribe(res => {
      this.staff = res;
    });
  }
  editStaffProfile() {
    this.router.navigate(['staff/edit-profile']);
  }
}


