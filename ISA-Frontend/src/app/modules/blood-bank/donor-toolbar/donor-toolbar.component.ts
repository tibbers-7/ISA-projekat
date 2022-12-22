import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-donor-toolbar',
  templateUrl: './donor-toolbar.component.html',
  styleUrls: ['./donor-toolbar.component.css']
})
export class DonorToolbarComponent implements OnInit {

  constructor(private router: Router,private authService: AuthService) { }

  ngOnInit(): void {
  }

  DonorHomeClick(){
    this.router.navigate(['/donor/homepage']);
  }

  
  FormClick(){
    this.router.navigate(['/donor/form']);
  }

  
  ProfileClick(){
    this.router.navigate(['/donor/profile']);
  }

  
  LogOutClick(){
    this.authService.logout();
    this.router.navigate(['/']);
  }



}
