import { Component, OnInit } from '@angular/core';
import { User } from '../../model/user.model';
import { UserService } from '../../services/user.service';


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
      console.log("created user!");
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
