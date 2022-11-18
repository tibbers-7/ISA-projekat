import { Component, OnInit } from '@angular/core';
import { BloodCenter } from '../../model/blood-center.model';
import { BloodCenterService } from '../../services/blood-center.service';

@Component({
  selector: 'center-register',
  templateUrl: './center-register.component.html',
  styleUrls: ['./center-register.component.css']
})
export class CenterRegisterComponent implements OnInit {

  public bloodCenter =new BloodCenter();
  public admins=[
    {name:'admin1'},
    {name:'admin2'},
    {name: 'admin3'}
  ]
  constructor(private bloodCenterService:BloodCenterService) { }

  ngOnInit(): void {
  }

  post() {

    this.bloodCenter.avgScore = 0.0;
    this.bloodCenterService.createCenter(this.bloodCenter).subscribe(res => {
      console.log("created center!");
    });
  }

  

}
