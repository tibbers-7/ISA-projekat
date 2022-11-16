import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, Params } from "@angular/router";
import { User } from "../model/user.model";
import { UserService } from "../services/user.service";

@Component({
  selector: 'app-edit-staff-profile',
  templateUrl: './edit-staff-profile.component.html',
  styleUrls: ['./edit-staff-profile.component.css']
})

export class EditStaffProfileComponent implements OnInit {

  public staff: User | undefined = undefined;

  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.userService.getUser(params['id']).subscribe(res => {
        this.staff = res;
      })
    });
  }

  public updateStaff(): void {
    if (!this.isValidInput()) return;
    this.userService.updateUser(this.staff).subscribe(res => {
      this.router.navigate(['staff/{id}/profile', { id: this.staff?.id }]);
    });
  }

  private isValidInput(): boolean {
    return this.staff?.password != '' && this.staff?.name != '' && this.staff?.email != '' && this.staff?.adress != '' && this.staff?.phoneNumber != '';
  }
}
