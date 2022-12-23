import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { MaterialModule } from "app/material/material.module";
import { BloodCenterProfileComponent } from "./blood-center-profile/blood-center-profile.component";
import { CentersListComponent } from './centers-list/centers-list/centers-list.component';
import { BloodDonorFormComponent } from './bloodDonor-form/blood-donor-form/blood-donor-form.component';
import { CenterRegisterComponent } from './center-register/center-register/center-register.component';
import { DonorProfileComponent } from "./donor-profile/donor-profile.component";
import { EditDonorProfileComponent } from './edit-donor-profile/edit-donor-profile.component';
import { UserListComponent } from './user-list/user-list.component';
import { StaffRegistrationComponent } from './staff-registration/staff-registration.component';
import { StaffProfileComponent } from './staff-profile/staff-profile.component';
import { EditStaffProfileComponent } from "./edit-staff-profile/edit-staff-profile.component";
import { BloodCenterEditComponent } from "./blood-center-edit/blood-center-edit.component";
import { AppointmentDialogComponent } from "./staff-appointment/appointment-dialog.component";
import { DonorHomepageComponent } from './donor-homepage/donor-homepage/donor-homepage.component';
import { AdminNewCenterComponent } from "./admin-new-center/admin-new-center/admin-new-center.component";
import { DonorFormComponent } from "./donor-form/donor-form/donor-form.component";
import { DonorToolbarComponent } from "./donor-toolbar/donor-toolbar.component";
import { StaffToolbarComponent } from "./staff-toolbar/staff-toolbar.component";
import { AdminToolbarComponent } from "./admin-toolbar/admin-toolbar.component";
import { AdminHomepageComponent } from './admin-homepage/admin-homepage.component';
import { StaffHomepageComponent } from './staff-homepage/staff-homepage/staff-homepage.component';
import { DonorAppointmentsComponent } from './donor-appointments/donor-appointments/donor-appointments.component'; 
import { DonorAppointmentScheduleComponent } from './donor-appointment-schedule/donor-appointment-schedule/donor-appointment-schedule.component'; 
import { DonorMadeAppointmentComponent } from "./donor-made-appointment/donor-made-appointment.component";
import { RoleGuardService } from '../../auth/role-guard.service';

const routes: Routes = [
  
  { path: 'donor/homepage', component: DonorHomepageComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' } },
  { path: 'donor/profile', component: DonorProfileComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' } },
  { path: 'donor/edit-profile', component: EditDonorProfileComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' } },
  {path: 'donor/form', component:DonorFormComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' }},
  { path: 'donor/schedule-appt', component:DonorAppointmentScheduleComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' }},
  { path: 'donor/appointments', component:DonorAppointmentsComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' }},
  { path: 'donor-homepage', component: DonorHomepageComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' } },
  { path: 'donor/make-appointment', component: DonorMadeAppointmentComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' } },

  { path: 'admin/homepage', component:AdminHomepageComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' }},
  { path: 'admin/user-list', component: UserListComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' } },
  { path: 'admin/center-register', component: CenterRegisterComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' } },
  {path: 'admin/new-center', component:AdminNewCenterComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' }},
  { path: 'admin/staff-register', component: StaffRegistrationComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' } },

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
    DonorProfileComponent,
    DonorMadeAppointmentComponent,
    BloodDonorFormComponent,
    CenterRegisterComponent,
    EditDonorProfileComponent,
    UserListComponent,
    StaffRegistrationComponent,
    StaffProfileComponent,
    EditStaffProfileComponent,
    BloodCenterEditComponent,
    DonorHomepageComponent,
    DonorFormComponent,
    AdminNewCenterComponent,
    DonorToolbarComponent,
    StaffToolbarComponent,
    AdminToolbarComponent,
    AdminHomepageComponent,
    StaffHomepageComponent,
    DonorAppointmentScheduleComponent,
    DonorAppointmentsComponent
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
    CenterRegisterComponent,
    BloodDonorFormComponent,
    StaffToolbarComponent,
    DonorToolbarComponent,
    AdminToolbarComponent
  ],
  entryComponents: [AppointmentDialogComponent]
})

export class BloodBankModule { }
