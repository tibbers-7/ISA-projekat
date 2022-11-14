import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Staff } from '../model/staff.model';
import { UserService} from '../services/user.service';
@Component({
  selector: 'app-staff-profile',
  templateUrl: './staff-profile.component.html',
  styleUrls: ['./staff-profile.component.css']
})
export class StaffProfileComponent {

  public staff: Staff | undefined;

  constructor(private userService: UserService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.userService.getStaffById(params['id']).subscribe(res => {
        this.staff = res;
      })
    });


  }
}
