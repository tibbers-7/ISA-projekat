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

  public dataSource = new MatTableDataSource<BloodCenter>();

  public centers: BloodCenter[] = [];

  constructor(private bloodService:BloodCenterService) { }

  ngOnInit(): void {
    this.bloodService.getCenters().subscribe(res => {
      this.dataSource.data = this.centers;
    });
  }

}
