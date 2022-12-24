import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../blood-bank/services/auth.service';

@Component({
  selector: 'admin-toolbar',
  templateUrl: './admin-toolbar.component.html',
  styleUrls: ['./admin-toolbar.component.css']
})
export class AdminToolbarComponent implements OnInit {

  constructor(private router:Router,private authService:AuthService) { }

  ngOnInit(): void {
  }

  AdminHomeClick(){
    this.router.navigate(['/admin/homepage']);
  }

  UsersClick(){
    this.router.navigate(['/admin/user-list']);
  }

  LogOutClick(){
    this.authService.logout();
    this.router.navigate(['/']);
  }

}
