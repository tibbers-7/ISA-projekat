import { Component, OnInit } from '@angular/core';
import { BloodCenter } from 'app/modules/model/blood-center.model';
import { BloodCenterService } from 'app/modules/services/blood-center.service';

@Component({
  selector: 'app-admin-new-center',
  templateUrl: './admin-new-center.component.html',
  styleUrls: ['./admin-new-center.component.css']
})
export class AdminNewCenterComponent implements OnInit {

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
