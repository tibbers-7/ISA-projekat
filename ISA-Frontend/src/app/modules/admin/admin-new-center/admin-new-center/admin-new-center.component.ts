import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BloodCenter } from 'app/model/blood-center.model';
import { BloodCenterService } from 'app/services/blood-center.service';

@Component({
  selector: 'app-admin-new-center',
  templateUrl: './admin-new-center.component.html',
  styleUrls: ['./admin-new-center.component.css']
})
export class AdminNewCenterComponent implements OnInit {

  public bloodCenter =new BloodCenter();
  public address:string='';
  public city:string='';
  public country:string='';

  public time:string='';
  constructor(private bloodCenterService: BloodCenterService) { }

  ngOnInit(): void {
   
  }

  post() {
    this.bloodCenter.avgScore = 0.0;
    console.log(this.time);
    this.bloodCenter.address=this.address+','+this.city+','+this.country;
    // this.bloodCenterService.createCenter(this.bloodCenter).subscribe(res => {
    //   console.log("created center!");
    // });
   
  }


}
