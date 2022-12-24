import { Component, OnInit } from '@angular/core';
import { Router,ActivatedRoute, Params } from '@angular/router';
import { Donor } from '../../model/donor.model';
import { HttpClient } from '@angular/common/http';
import { DonorService } from '../../services/donor.service';

@Component({
  selector: 'app-edit-donor-profile',
  templateUrl: './edit-donor-profile.component.html',
  styleUrls: ['./edit-donor-profile.component.css']
})
export class EditDonorProfileComponent {
  
  public donor = new Donor();

  apiHost: string = 'http://localhost:16177/';

  constructor(private donorService: DonorService, private route: ActivatedRoute, private router: Router, private httpClient: HttpClient) { }

  ngOnInit(): void {
   //promeniti kad dodje localstorage
    this.donorService.getDonor(1).subscribe(res => {
      this.donor = res;
    });
   
  }
  edit(): void {
    this.donorService.updateDonor(this.donor).subscribe(res => {
      this.router.navigate(['donor/profile']);
    });
  }
}
