import { Component, Inject, Injectable } from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';

@Component({

  templateUrl: 'staff-appointment.component.html'
  styleUrls: 'staff-appointment.component.css'
})
export class MessageComponent {
  constructor(private dialogRef: MatDialogRef<MessageComponent>, @Inject(MAT_DIALOG_DATA) public data: any) {
  }
  public closeMe() {
    this.dialogRef.close();
  }
}
