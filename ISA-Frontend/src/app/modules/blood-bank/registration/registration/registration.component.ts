import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/modules/blood-bank/model/user.model';
import { UserService } from 'src/app/modules/blood-bank/services/user.service';


@Component({
  selector: 'registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  public user=new User();

  constructor(private userService:UserService) { }

  ngOnInit(): void {

  }

  post()  {
    if (!this.checkValidity()) {
      console.log("Missing parameters!");
      return;
    }

    this.userService.createUser(this.user).subscribe(res => {
      // dodati toast
    });
  }

  checkValidity(){
    if(this.user.email==='' || this.user.adress==='' || this.user.gender==='' 
      || this.user.jmbg==='' || this.user.name==='' || this.user.password===''
      || this.user.phoneNumber==='' || this.user.workplace==='') 
      return false;

    return true;
  }
}
