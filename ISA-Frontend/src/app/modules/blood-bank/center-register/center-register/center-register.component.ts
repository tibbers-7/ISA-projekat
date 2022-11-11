import { Component, OnInit } from '@angular/core';
import { BloodCenter } from '../../model/blood-center.model';
import { BloodCenterService } from '../../services/blood-center.service';

@Component({
  selector: 'app-center-register',
  templateUrl: './center-register.component.html',
  styleUrls: ['./center-register.component.css']
})
export class CenterRegisterComponent implements OnInit {

  public bloodCenter=new BloodCenter();
  constructor(private bloodCenterService:BloodCenterService) { }

  ngOnInit(): void {
  }

  post(){
    
  }

}
