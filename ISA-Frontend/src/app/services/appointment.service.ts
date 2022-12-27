import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Appointment } from '../model/appointment.model';
import { BloodCenter } from '../model/blood-center.model';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.apiHost + 'api/Appointment', { headers: this.headers });
  }

  getAppointment(id: number): Observable<Appointment> {
    return this.http.get<Appointment>(this.apiHost + 'api/Appointment/' + id, { headers: this.headers });
  }

  newAppointment(appointment: Appointment): Observable<any> {
    return this.http.post<any>(this.apiHost + 'api/Appointment/new', appointment, { headers: this.headers });
  }

  scheduleAppt(appointment: Appointment): Observable<any> {
    return this.http.post<any>(this.apiHost + 'api/Appointment/schedule', appointment, { headers: this.headers });
  }

  cancelAppt(appointment: Appointment): Observable<any> {
    return this.http.post<any>(this.apiHost + 'api/Appointment/cancel', appointment, { headers: this.headers });
  }

  getCentersForDateTime(dateTime: string): Observable<BloodCenter[]> {
    return this.http.get<BloodCenter[]>(this.apiHost + 'api/Appointment/centers/' + dateTime, { headers: this.headers });
  }

  getAvailableByCenter(centerId : number): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.apiHost + 'api/Appointment/available/' + centerId, { headers: this.headers });
  }

  getScheduledByCenter(centerId : number): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.apiHost + 'api/Appointment/center/scheduled/' + centerId, { headers: this.headers });
  }
  getScheduledForDonor(donorId : number): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.apiHost + 'api/Appointment/donor/scheduled/' + donorId, { headers: this.headers });
  }
  getAvailableForDonor(centerId:number,donorId : number): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.apiHost + 'api/Appointment/donor/available/' + donorId+"/"+centerId, { headers: this.headers });
  }
  
  getQR(appointment:Appointment): Observable<any> {
    return this.http.post<any>(this.apiHost + 'api/Appointment/qr', appointment, { headers: this.headers });
  }

  

 

}
