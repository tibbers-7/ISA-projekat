import { Component, OnInit, ViewChild } from '@angular/core';
import { BloodCenter } from '../../../../model/blood-center.model';
import { BloodCenterService } from '../../../../services/blood-center.service';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import {MatSort, Sort} from '@angular/material/sort';


@Component({
  selector: 'centers-list',
  templateUrl: './centers-list.component.html',
  styleUrls: ['./centers-list.component.css']
})
export class CentersListComponent implements OnInit {

  @ViewChild('empTbSort') empTbSort = new MatSort();
  public centers: BloodCenter[] = [];
  public searchText: any= "";
  public filterScore: any= "";
  public filterCity: any="";
  public open: any="";
  public dataSource = new MatTableDataSource<BloodCenter>();
  public cities: string[]=[];
  public displayedColumns = ['name','adress','description','avgScore','openHours'];
  

  constructor(private bloodService:BloodCenterService) { }

  ngOnInit(): void {
    this.bloodService.getCenters().subscribe(res => {
      this.centers = res;
      this.dataSource.data = this.centers;
      this.dataSource.sort = this.empTbSort;
      
    });
    this.loadCities();
    const centersCopy = [...this.centers]; 
    this.centers.sort((b,a) => (
    // your sort logic
    a.avgScore - b.avgScore // example : order by id    
    ));

  }
  loadCities()
  {
      this.bloodService.getCities().subscribe(res=>{
        this.cities=res;
      })
  }
  applySearch(event: Event) {
    this.dataSource.filterPredicate = function (centers,filter) {
    return centers.name.toLocaleLowerCase().startsWith(filter.toLocaleLowerCase());
    //||  centers.address.toLocaleLowerCase().includes(filter.toLocaleLowerCase());
    }
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }


 filterByScore(event: Event) {
    this.dataSource.filterPredicate = function (centers,filter) {
    return centers.avgScore > parseFloat(filter);
}
   const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
 }

 filterOpen(event: Event) {
    this.dataSource.filterPredicate = function (centers,filter) {
     if((event.target as HTMLInputElement).value == 'open')
        return true;
      else return true;
      }
    let dateTime = new Date()
    const filterValue = dateTime.getHours.toString();
   this.dataSource.filter = filterValue.trim().toLowerCase();
  }


  filterByCity(event: Event) {
    this.dataSource.filterPredicate = function (centers,filter) {
    return true;
   }
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
