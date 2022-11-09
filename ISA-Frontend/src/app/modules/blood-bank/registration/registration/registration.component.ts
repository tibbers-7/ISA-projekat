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

}
