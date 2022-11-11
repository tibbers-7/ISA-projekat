import { Component, OnInit } from '@angular/core';
import { BloodCenter } from '../../model/blood-center.model';
import { BloodCenterService } from '../../services/blood-center.service';
import { MatTableDataSource } from '@angular/material/table';


@Component({
  selector: 'centers-list',
  templateUrl: './centers-list.component.html',
  styleUrls: ['./centers-list.component.css']
})
export class CentersListComponent implements OnInit {

  
  public centers: BloodCenter[] = [];

  constructor(private bloodService:BloodCenterService) { }

  ngOnInit(): void {
    this.bloodService.getCenters().subscribe(res => {
      this.centers = res;
    });

    const centersCopy = [...this.centers]; 
    this.centers.sort((a, b) => (
    // your sort logic
    a.avgScore - b.avgScore // example : order by id

  ));
  }
  

}
