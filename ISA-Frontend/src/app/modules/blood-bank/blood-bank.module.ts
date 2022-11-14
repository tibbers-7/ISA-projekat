import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { MaterialModule } from "app/material/material.module";
import { BloodCenterProfileComponent } from "./blood-center-profile/blood-center-profile.component";
import { RegistrationComponent } from './registration/registration/registration.component';
import { CentersListComponent } from './centers-list/centers-list/centers-list.component';
import { BloodDonorFormComponent } from './bloodDonor-form/blood-donor-form/blood-donor-form.component';
import { CenterRegisterComponent } from './center-register/center-register/center-register.component';
import { UserProfileComponent } from "./user-profile/user-profile.component";
import { EditUserProfileComponent } from './edit-user-profile/edit-user-profile.component';
import { UserListComponent } from './user-list/user-list.component';
import { StaffRegistrationComponent } from './staff-registration/staff-registration.component';
import { MatSortModule } from '@angular/material/sort';
import { StaffProfileComponent } from './staff-profile/staff-profile.component';
import { EditStaffProfileComponent } from "./edit-staff-profile/edit-staff-profile.component";

const routes: Routes = [
  { path: 'register', component: RegistrationComponent },
  { path: 'user-profile/:id', component: UserProfileComponent },
  { path: 'edit-user-profile/:id', component: EditUserProfileComponent },
  { path: 'user-list', component: UserListComponent },
  { path: 'staff-register', component: StaffRegistrationComponent },
  { path: 'staff/:id/profile', component: StaffProfileComponent },
  { path: 'staff/:id/center', component: BloodCenterProfileComponent },
  { path: 'staff/:id/edit-profile', component: EditStaffProfileComponent }

  
  
];

@NgModule({
  declarations: [
    BloodCenterProfileComponent,
    RegistrationComponent,
    CentersListComponent,
    UserProfileComponent,
    BloodDonorFormComponent,
    CenterRegisterComponent,
    EditUserProfileComponent,
    UserListComponent,
    StaffRegistrationComponent,
    StaffProfileComponent,
    EditStaffProfileComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    MatSortModule
  ],
  exports: [ 
    RouterModule,
    RegistrationComponent,
    CentersListComponent,
    UserProfileComponent,
    BloodDonorFormComponent,
    EditUserProfileComponent,
    UserListComponent,
    BloodDonorFormComponent,
    CenterRegisterComponent,
    StaffRegistrationComponent,
    StaffProfileComponent,
    EditStaffProfileComponent
   ]
})

export class BloodBankModule { }
