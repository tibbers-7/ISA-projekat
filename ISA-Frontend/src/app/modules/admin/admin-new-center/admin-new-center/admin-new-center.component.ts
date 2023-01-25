import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Address } from 'app/model/address.model';
import { BloodCenter } from 'app/model/blood-center.model';
import { BloodCenterService } from 'app/services/blood-center.service';

@Component({
  selector: 'app-admin-new-center',
  templateUrl: './admin-new-center.component.html',
  styleUrls: ['./admin-new-center.component.css']
})
export class AdminNewCenterComponent implements OnInit {

  public bloodCenter =new BloodCenter();
  public address = new Address();
  public startTime:string='';
  public endTime:string='';
  constructor(private bloodCenterService: BloodCenterService) { }

  ngOnInit(): void {
   
  }

  post() {
    this.bloodCenter.avgScore = 0.0;
    this.address.centerId = this.bloodCenter.id;
    this.bloodCenterService.createAddress(this.address).subscribe();  
     this.bloodCenterService.createCenter(this.bloodCenter).subscribe(res => {
      console.log("created center!");
     });
   
  }


}
