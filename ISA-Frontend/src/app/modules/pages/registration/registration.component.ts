import { Component, OnInit } from '@angular/core';
//import { NgToastService } from 'ng-angular-popup';
import { RegDTO } from 'app/model/regDTO.model';
import { User } from 'app/model/user.model';
import { AuthService } from 'app/services/auth.service';
import { DonorService } from 'app/services/donor.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  
  public user=new RegDTO();
  public passwordConfirm:string='';

  constructor( private authService: AuthService, private donorService:DonorService,private router: Router, private toast:NgToastService) {
  }

  ngOnInit(): void {
    
  }

  post()  {
    
    if(!this.checkValidity()) return;
    console.log("validno je");
    this.user.userType='DONOR';
    this.authService.register(this.user)
      .subscribe(res => {
        console.log("uspelo jeej");
        this.toast.success({detail:"Sent activation link!",summary:'Check your email.',duration:5000});
    }, error=>{
      console.log(error.message);
    });

  }


  checkValidity(){
    if (this.user.email === '' || this.user.address==='' || this.user.gender==='' || this.user.jmbg===0 || this.user.name==='' || this.user.password==='' || this.user.workplace==='' || this.user.city==='' || this.user.state==='' || this.user.employmentInfo==='') {
      this.toast.error({detail:'Required fields are empty!',summary:"Please complete the form.",duration:5000});
      return false;
    }

    if (this.user.password != this.passwordConfirm){
      this.toast.error({detail:'Passwords don\'t match',summary:"Please retype the password.",duration:5000});
      return false;
    }


    if (isNaN(Number(this.user.jmbg))){
      this.toast.error({detail:'Jmbg contains only numbers!',summary:"Please enter a valid jmbg.",duration:5000});
      return false;
    }
    return true;
  }

}
