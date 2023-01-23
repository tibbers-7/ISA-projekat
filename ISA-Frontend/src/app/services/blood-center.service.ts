import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Address } from 'app/model/address.model';
import { Observable } from 'rxjs';
import { BloodCenter } from '../model/blood-center.model';
import { Donor } from '../model/donor.model';

@Injectable({
  providedIn: 'root'
})
export class BloodCenterService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getCenters(): Observable<BloodCenter[]> {
    return this.http.get<BloodCenter[]>(this.apiHost + 'api/BloodCenter', { headers: this.headers });
  }

  createCenter(bloodCenter: any): Observable<any> {
    return this.http.post<any>(this.apiHost + 'api/BloodCenter', bloodCenter, { headers: this.headers });
  }

  getCenter(id: number): Observable<BloodCenter> {
    return this.http.get<BloodCenter>(this.apiHost + 'api/BloodCenter/' + id, { headers: this.headers });
  }

  getCities(): Observable<string[]> {
    return this.http.get<string[]>(this.apiHost + 'api/BloodCenter/cities',  { headers: this.headers });
  }
 
  updateCenter(bloodCenter: any): Observable<any> {
    return this.http.put<any>(this.apiHost + 'api/BloodCenter/' + bloodCenter.id, bloodCenter, { headers: this.headers });
  }

  deleteCenter(id: any): Observable<any> {
    return this.http.delete<any>(this.apiHost + 'api/BloodCenter/' + id, { headers: this.headers });
  }

  getDonorsForCenter(id: number): Observable<Donor[]> {
    return this.http.get<Donor[]>(this.apiHost + 'api/BloodCenter/' + id + '/donors', { headers: this.headers });
  }

  getAddressForCenter(id: number):Observable<Address> {
    return this.http.get<Address>(this.apiHost+'api/BloodCenter/address/'+id, { headers: this.headers });
  }

  updateAddress(address: any): Observable<any> {
    return this.http.put<any>(this.apiHost + 'api/BloodCenter/address/' + address.id, address, { headers: this.headers });
  }

  createAddress(address: any): Observable<any> {
    return this.http.post<any>(this.apiHost + 'api/address', address, { headers: this.headers });
  }


}
