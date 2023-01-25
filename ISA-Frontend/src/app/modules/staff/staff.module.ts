import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule,Routes } from '@angular/router';
import { RoleGuardService } from 'app/auth/role-guard.service';
import { MaterialModule } from 'app/material/material.module';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDialogModule } from '@angular/material/dialog';

import { PagesModule } from '../pages/pages.module';

import { StaffHomepageComponent } from './staff-homepage/staff-homepage/staff-homepage.component';
import { StaffProfileComponent } from './staff-profile/staff-profile.component';
import { BloodCenterProfileComponent } from './blood-center-profile/blood-center-profile.component';
import { EditStaffProfileComponent } from './edit-staff-profile/edit-staff-profile.component';
import { BloodCenterEditComponent } from './blood-center-edit/blood-center-edit.component';
import { StaffToolbarComponent } from './staff-toolbar/staff-toolbar.component';
import { AppointmentDialogComponent } from './staff-appointment/appointment-dialog.component';
import { ChangePasswordComponent } from './change-password/change-password.component';



const routes: Routes = [

  { path: 'staff/homepage', component:StaffHomepageComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' }},
  { path: 'staff/profile', component: StaffProfileComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' } },
  { path: 'staff/center', component: BloodCenterProfileComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' } },
  { path: 'staff/edit-profile', component: EditStaffProfileComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' } },
  { path: 'staff/edit-center', component: BloodCenterEditComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' } },
  { path: 'staff/change-password', component: ChangePasswordComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' } },
 
];

@NgModule({
  declarations: [
    StaffHomepageComponent,
    StaffToolbarComponent,
    StaffProfileComponent,
    BloodCenterEditComponent,
    BloodCenterProfileComponent,
    EditStaffProfileComponent,
    AppointmentDialogComponent,
    ChangePasswordComponent
  ],
  imports: [
    CommonModule,
    PagesModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    RouterModule.forChild(routes),
  ],
  entryComponents: [AppointmentDialogComponent]
  
})
export class StaffModule { }
