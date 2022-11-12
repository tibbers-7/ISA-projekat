import { Component, OnInit, ViewChild } from '@angular/core';
import { BloodCenter } from '../../model/blood-center.model';
import { BloodCenterService } from '../../services/blood-center.service';
import { MatTableDataSource } from '@angular/material/table';
import {MatSort, Sort} from '@angular/material/sort';


@Component({
  selector: 'centers-list',
  templateUrl: './centers-list.component.html',
  styleUrls: ['./centers-list.component.css']
})
export class CentersListComponent implements OnInit {

  @ViewChild('empTbSort') empTbSort = new MatSort();
  public centers: BloodCenter[] = [];

  public dataSource = new MatTableDataSource<BloodCenter>();
  public displayedColumns = ['name','adress','description','avgScore'];

  constructor(private bloodService:BloodCenterService) { }

  ngOnInit(): void {
    this.bloodService.getCenters().subscribe(res => {
      this.centers = res;
      this.dataSource.data = this.centers;
      this.dataSource.sort = this.empTbSort;
    });


    const centersCopy = [...this.centers]; 
    this.centers.sort((b,a) => (
    // your sort logic
    a.avgScore - b.avgScore // example : order by id

  ));
  }
  

}
