import { Component, OnInit } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { RegDTO } from 'app/modules/blood-bank/model/regDTO.model';
import { User } from 'app/modules/blood-bank/model/user.model';
import { AuthService } from 'app/modules/blood-bank/services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  
  public user=new RegDTO();
  public allergies=[
    {name:"polen",checked:false},
    {name:"grinje",checked:false}
  ]
  public bloodTypes=['A','B','AB','O'];
  
  public selectedAllergies:any[]=[];

  constructor(private toast: NgToastService, private authService: AuthService, private patientService:PatientService) {
  }

  ngOnInit(): void {
    
  }

  post()  {
    this.user.allergies=this.selectedAllergies;
    
    if(!this.checkValidity()) return;
    console.log("validno je");
    this.patientService.registerPatient(this.user)
      .subscribe(res => {
        this.toast.success({detail:"Added patient to db!",summary:'',duration:5000});
    }, error=>{
      console.log(error.message);
    });

    //Logika premestena u registraciju
    // this.authService.register(this.user).subscribe(res => {
    //   this.toast.success({detail:"Succesfully registered!",summary:'Verification link was sent to your email.',duration:5000});
    // });
  }


  checkValidity(){
    if (this.user.email === '' || this.user.address==='' || this.user.gender==='' || this.user.jmbg==='' || this.user.name==='' || this.user.password==='') {
      this.toast.error({detail:'Required fields are empty!',summary:"Please complete the form.",duration:5000});
      return false;
    }

    
    console.log(this.selectedAllergies);
    if(this.displayAllergies){
      if(this.selectedAllergies.length===0){
        this.toast.error({detail:'No allergies were selected!',summary:"Add allergies, or check 'No allergies'.",duration:5000});
        return false;
      }
    }

    if (isNaN(Number(this.user.jmbg))){
      this.toast.error({detail:'Jmbg contains only numbers!',summary:"Please enter a valid jmbg.",duration:5000});
      return false;
    }
    return true;
  }

  displayAllergies=false;
  showAllergies(){
    this.displayAllergies=true;
  }

  hideAllergies(){
    this.displayAllergies=false;
  }

  addAllergies(){
    this.selectedAllergies=this.allergies.filter(allergy=>allergy.checked);
    //console.log(this.selectedAllergies);
  }

}
