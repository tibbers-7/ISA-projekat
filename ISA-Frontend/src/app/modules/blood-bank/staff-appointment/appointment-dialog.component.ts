import { Component, Inject, Injectable, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import * as moment from 'moment';
import { Observable } from 'rxjs';
import { range } from 'rxjs';
import { Staff } from '../model/staff.model';
import { StaffService } from '../services/staff.service';


@Component({
  selector:'app-appointment-dialog',
  templateUrl: './appointment-dialog.component.html',
  styleUrls: ['./appointment-dialog.component.css']
})
export class AppointmentDialogComponent implements OnInit {

  form!: FormGroup;
  minDate = new Date();
  allStaff: Staff[] = [];
  numbers: number[] = [];

    constructor(private dialogRef: MatDialogRef <AppointmentDialogComponent>, private staffService: StaffService, private fb: FormBuilder) {}

  ngOnInit(): void {

    for (let i = 15; i <= 60; i++){ this.numbers.push(i); }
    this.staffService.getAll().subscribe(res => {
      this.allStaff = res;
    });

    this.form = this.fb.group({
      dateTime: [null],
      duration: [''],
      staff: ['']
    });
  }

  public save() {
    this.dialogRef.close(this.form?.value);
  }

  public close() {}
}
