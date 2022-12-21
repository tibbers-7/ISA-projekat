import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Donor } from '../model/donor.model';
import { DonorService } from '../services/donor.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-donor-profile',
  templateUrl: './donor-profile.component.html',
  styleUrls: ['./donor-profile.component.css']
})
export class DonorProfileComponent {
  
  public donor: Donor=new Donor();

  constructor(private donorService: DonorService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {

    //kad napravimo localstorage
    this.donorService.getDonor(1).subscribe(res => {
      this.donor = res;
    });
 
  }
  goToEditPage() {
    this.router.navigate(['/edit-donor-profile'])
  }
}
