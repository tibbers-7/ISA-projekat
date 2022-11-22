import { Component, OnInit } from '@angular/core';
import { BloodCenter } from '../../model/blood-center.model';
import { User } from '../../model/user.model';
import { BloodCenterService } from '../../services/blood-center.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'center-register',
  templateUrl: './center-register.component.html',
  styleUrls: ['./center-register.component.css']
})
export class CenterRegisterComponent implements OnInit {

  public bloodCenter =new BloodCenter();
  
  constructor(private bloodCenterService: BloodCenterService) { }

  ngOnInit(): void {
   
  }

  post() {
    this.bloodCenter.avgScore = 0.0;
    this.bloodCenterService.createCenter(this.bloodCenter).subscribe(res => {
      console.log("created center!");
    });
   
  }

}
