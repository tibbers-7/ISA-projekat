import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { BloodCenter } from '../model/blood-center.model';
import { BloodCenterService } from '../services/blood-center.service';
import { User } from '../model/user.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-blood-center-profile',
  templateUrl: './blood-center-profile.component.html',
  styleUrls: ['./blood-center-profile.component.css']
})
export class BloodCenterProfileComponent {
  
  public center: BloodCenter | undefined;
  public staff: User | undefined;

  constructor(private bloodCenterService: BloodCenterService, private userService: UserService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.userService.getUser(params['id']).subscribe(res => {
        this.staff = res;

        this.bloodCenterService.getCenter(this.staff.idOfCenter).subscribe(res => {
          this.center = res;
        })
      });

      })

    


    
  }
}
