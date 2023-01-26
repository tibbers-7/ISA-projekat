import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Address } from 'app/model/address.model';
import { Observable } from 'rxjs';
import { BloodCenter } from '../model/blood-center.model';
import { Donor } from '../model/donor.model';

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {

  apiHost: string = 'http://localhost:44371/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  startDelivery(): Observable<any> {
    return this.http.get<any>(this.apiHost + '/Location', { headers: this.headers });
  }

}