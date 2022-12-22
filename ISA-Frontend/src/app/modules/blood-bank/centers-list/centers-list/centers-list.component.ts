import { Component, OnInit, ViewChild } from '@angular/core';
import { BloodCenter } from '../../model/blood-center.model';
import { BloodCenterService } from '../../services/blood-center.service';
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
    const centersCopy = [...this.centers]; 
    this.centers.sort((b,a) => (
    // your sort logic
    a.avgScore - b.avgScore // example : order by id    
    ));

  }
  loadCities(event: Event)
  {
    this.cities=[];
    this.centers.forEach(element => 
      {
        const address=element.adress;
        const city=address.slice(0,address.indexOf(" "));
        this.cities.push(city);
      }
      );
  }
  applySearch(event: Event) {
    this.dataSource.filterPredicate = function (centers,filter) {
      return centers.name.toLocaleLowerCase().startsWith(filter.toLocaleLowerCase()) ||  centers.adress.toLocaleLowerCase().includes(filter.toLocaleLowerCase());
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
        return parseFloat(centers.openHours.substring(0,2)) > parseFloat(filter) && parseFloat(centers.openHours.substring(6,8)) < parseFloat(filter);
      else
        return !(parseFloat(centers.openHours.substring(0,2)) > parseFloat(filter) && parseFloat(centers.openHours.substring(6,8)) < parseFloat(filter));
    }
    let dateTime = new Date()
    const filterValue = dateTime.getHours.toString();
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  filterByCity(event: Event) {
    this.dataSource.filterPredicate = function (centers,filter) {
      return centers.adress.toLocaleLowerCase().startsWith(filter.toLocaleLowerCase());
    }
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
