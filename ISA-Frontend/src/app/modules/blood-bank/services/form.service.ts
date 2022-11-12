import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Form } from '../model/form.model';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getForms(): Observable<Form[]> {
    return this.http.get<Form[]>(this.apiHost + 'api/Form', { headers: this.headers });
  }

  getForm(id: number): Observable<Form> {
    return this.http.get<Form>(this.apiHost + 'api/Form/' + id, { headers: this.headers });
  }
  createForm(form: any): Observable<any> {
    return this.http.post<any>(this.apiHost + 'api/Form', form, { headers: this.headers });
  }

  updateUser(form: any): Observable<any> {
    
    return this.http.put<any>(this.apiHost + 'api/Form/' + form.donorId, form, { headers: this.headers });
  }

}
