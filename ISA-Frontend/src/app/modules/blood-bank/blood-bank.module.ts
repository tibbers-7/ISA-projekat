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
import { MatSortModule } from '@angular/material/sort';
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
import {MatToolbarModule} from '@angular/material/toolbar';
import { AdminHomepageComponent } from './admin-homepage/admin-homepage.component';
import { StaffHomepageComponent } from './staff-homepage/staff-homepage/staff-homepage.component'; 

const routes: Routes = [
  
  { path: 'donor/homepage', component: DonorHomepageComponent},
  { path: 'donor/profile', component: DonorProfileComponent },
  { path: 'donor/edit-profile', component: EditDonorProfileComponent },
  {path: 'donor/form', component:DonorFormComponent},


  { path: 'admin/homepage', component:AdminHomepageComponent},
  { path: 'admin/user-list', component: UserListComponent },
  { path: 'admin/center-register', component: CenterRegisterComponent },
  {path: 'admin/new-center', component:AdminNewCenterComponent},
  { path: 'admin/staff-register', component: StaffRegistrationComponent },

  { path: 'staff/homepage', component:StaffHomepageComponent},
  { path: 'staff/profile', component: StaffProfileComponent },
  { path: 'staff/center', component: BloodCenterProfileComponent },
  { path: 'staff/edit-profile', component: EditStaffProfileComponent },
  { path: 'staff/edit-center', component: BloodCenterEditComponent },
  
  { path: 'center-list', component: CentersListComponent },
  
  
  

];

@NgModule({
  declarations: [
    BloodCenterProfileComponent,
    AppointmentDialogComponent,
    CentersListComponent,
    DonorProfileComponent,
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
    StaffHomepageComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    MatSortModule,
    MatToolbarModule
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
