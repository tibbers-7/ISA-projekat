import { Component, OnInit } from '@angular/core';
import { User } from '../../../model/user.model';
import { BloodCenter } from '../../../model/blood-center.model';
import { UserService } from '../../../services/user.service';
import { BloodCenterService } from '../../../services/blood-center.service';
import { AuthService } from 'app/services/auth.service';
import { RegDTO } from 'app/model/regDTO.model';
import { NgToastService } from 'ng-angular-popup';


@Component({
  selector: 'staff-registration',
  templateUrl: './staff-registration.component.html',
  styleUrls: ['./staff-registration.component.css']
})
export class StaffRegistrationComponent implements OnInit {

  public user=new RegDTO();
  public centers : BloodCenter[]= [];
  public selectedCenter: BloodCenter=new BloodCenter;
  constructor(private userService:UserService, private bloodService:BloodCenterService, private authService:AuthService,private toast:NgToastService) { }

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

    this.authService.register(this.user)
      .subscribe(res => {
        this.toast.success({detail:"Sent activation link!",summary:'Check your email.',duration:5000});
    }, error=>{
      console.log(error.message);
    });
  }

  setCenter(event:any)
  {
    console.log(this.selectedCenter.id);
    this.user.idOfCenter=this.selectedCenter.id;
    this.user.employmentInfo=this.selectedCenter.name;
  }
  checkValidity(){
    if(this.user.email==='' || this.user.address==='' || this.user.gender==='' 
      || this.user.jmbg===0 || this.user.name==='' || this.user.password===''
      || this.user.phoneNum==='') 
      return false;

    return true;
  }
}
