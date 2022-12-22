import { Component, OnInit } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { RegDTO } from 'app/modules/blood-bank/model/regDTO.model';
import { User } from 'app/modules/blood-bank/model/user.model';
import { AuthService } from 'app/modules/blood-bank/services/auth.service';
import { DonorService } from 'app/modules/blood-bank/services/donor.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  
  public user=new RegDTO();
  public passwordConfirm:string='';

  constructor(private toast: NgToastService, private authService: AuthService, private donorService:DonorService) {
  }

  ngOnInit(): void {
    
  }

  post()  {
    
    if(!this.checkValidity()) return;
    console.log("validno je");
    this.authService.register(this.user)
      .subscribe(res => {
        console.log("uspelo jeej");
        this.toast.success({detail:"Added patient to db!",summary:'',duration:5000});
    }, error=>{
      console.log(error.message);
    });

  }


  checkValidity(){
    if (this.user.email === '' || this.user.address==='' || this.user.gender==='' || this.user.jmbg==='' || this.user.name==='' || this.user.password==='') {
      this.toast.error({detail:'Required fields are empty!',summary:"Please complete the form.",duration:5000});
      return false;
    }


    if (isNaN(Number(this.user.jmbg))){
      this.toast.error({detail:'Jmbg contains only numbers!',summary:"Please enter a valid jmbg.",duration:5000});
      return false;
    }
    return true;
  }

}