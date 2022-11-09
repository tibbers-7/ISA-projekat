import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { BloodCenter } from 'src/app/modules/blood-bank/model/blood-center.model';
import { BloodCenterService } from 'src/app/modules/blood-bank/services/blood-center.service';

@Component({
  selector: 'app-blood-center-profile',
  templateUrl: './blood-center-profile.component.html',
  styleUrls: ['./blood-center-profile.component.css']
})
export class BloodCenterProfileComponent {

  public center: BloodCenter | undefined;

  constructor(private bloodCenterService: BloodCenterService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.bloodCenterService.getCenter(params['id']).subscribe(res => {
        this.center = res;
      })
    });
  }
}
