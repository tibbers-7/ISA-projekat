import { Component, OnInit } from '@angular/core';
//import { NgToastService } from 'ng-angular-popup';
import { User } from 'app/modules/blood-bank/model/user.model';
import { AuthService } from 'app/modules/blood-bank/services/auth.service';
import { Router } from '@angular/router';
import { RegDTO } from 'app/modules/blood-bank/model/regDTO.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public user=new RegDTO();
  public variable='';

  constructor(private router: Router,private authService:AuthService) { }

  ngOnInit(): void {
  }

  login(){
    if (!this.checkValidity()) return;

    this.authService
      .login(this.user)
      .subscribe(response => {
        this.authService.setSession(response);
        let role = this.authService.getRole();
        switch(role){
          case('DONOR' || 'donor'):{
            this.router.navigate(['/donor/homepage']);
            break;
          }
          case('ADMIN' || 'admin'):{
            this.router.navigate(['/admin/homepage']);
            break;
          }
          case('STAFF' || 'staff'):{
            this.router.navigate(['/staff/homepage']);
            break;
          }
          default:{
          //  this.toast.error({ detail: 'Unknown user type!', summary: "Please try again.", duration: 5000 });
          }
        }
      },
      error=>{
     //   this.toast.error({ detail: 'Incorrect email or password!', summary: "Please try again.", duration:5000});
          return;
      });
  }

  checkValidity(){
    if (this.user.email === '' || this.user.password==='') {
    //  this.toast.error({detail:'Required fields are empty!',summary:"Please complete the form.",duration:5000});
      return false;
    }
    return true;
  }

}
