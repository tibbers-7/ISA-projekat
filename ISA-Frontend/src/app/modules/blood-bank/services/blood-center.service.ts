import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BloodCenter } from '../model/blood-center.model';

@Injectable({
  providedIn: 'root'
})
export class BloodCenterService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getCenters(): Observable<BloodCenter[]> {
    return this.http.get<BloodCenter[]>(this.apiHost + 'api/bloodCenters', { headers: this.headers });
  }

  getCenter(id: number): Observable<BloodCenter> {
    return this.http.get<BloodCenter>(this.apiHost + 'api/bloodCenters/' + id, { headers: this.headers });
  }


}
