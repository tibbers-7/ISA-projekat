import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { MaterialModule } from "app/material/material.module";
import { BloodCenterProfileComponent } from "./blood-center-profile/blood-center-profile.component";
import { CentersListComponent } from './centers-list/centers-list/centers-list.component';
import { StaffProfileComponent } from './staff-profile/staff-profile.component';
import { EditStaffProfileComponent } from "./edit-staff-profile/edit-staff-profile.component";
import { BloodCenterEditComponent } from "./blood-center-edit/blood-center-edit.component";
import { AppointmentDialogComponent } from "./staff-appointment/appointment-dialog.component";
import { StaffToolbarComponent } from "./staff-toolbar/staff-toolbar.component";
import { StaffHomepageComponent } from './staff-homepage/staff-homepage/staff-homepage.component';
import { RoleGuardService } from '../../auth/role-guard.service';

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
  
  { path: 'center-list', component: CentersListComponent },
  
  
  

];

@NgModule({
  declarations: [
    BloodCenterProfileComponent,
    AppointmentDialogComponent,
    CentersListComponent,
    StaffProfileComponent,
    EditStaffProfileComponent,
    BloodCenterEditComponent,
    StaffToolbarComponent,
    
    
    StaffHomepageComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
   
  ],
  exports: [RouterModule,
    CentersListComponent,
    StaffToolbarComponent,
    
  ],
  entryComponents: [AppointmentDialogComponent]
})

export class BloodBankModule { }
