import { Component, OnInit } from '@angular/core';
import { User } from '../model/user.model';
import { BloodCenter } from '../model/blood-center.model';
import { UserService } from '../services/user.service';
import { BloodCenterService } from '../services/blood-center.service';


@Component({
  selector: 'staff-registration',
  templateUrl: './staff-registration.component.html',
  styleUrls: ['./staff-registration.component.css']
})
export class StaffRegistrationComponent implements OnInit {

  public user=new User();
  public centers : BloodCenter[]= [];
  constructor(private userService:UserService, private bloodService:BloodCenterService) { }

  ngOnInit(): void {
    this.bloodService.getCenters().subscribe(res => {
      this.centers = res;      
    });
  }

  post()  {
    if (!this.checkValidity()) {
      
      console.log("Missing parameters!");
      
      return;
    }
    this.user.userType='STAFF';
    this.user.profession='STAFF';
    this.userService.createUser(this.user).subscribe(res => {
      console.log("created user!");
    });
  }

  setCenter(event: Event)
  {
    this.user.workplace = (event.target as HTMLInputElement).value;
    this.centers.forEach(element => {
      if(element.name=this.user.workplace)
      {
        this.user.idOfCenter=element.id;
      }
    });
  }
  checkValidity(){
    if(this.user.email==='' || this.user.adress==='' || this.user.gender==='' 
      || this.user.jmbg==='' || this.user.name==='' || this.user.password===''
      || this.user.phoneNumber==='') 
      return false;

    return true;
  }
}
