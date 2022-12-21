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
import { DonorProfileComponent } from "./donor-profile/donor-profile.component";
import { EditDonorProfileComponent } from './edit-donor-profile/edit-donor-profile.component';
import { UserListComponent } from './user-list/user-list.component';
import { StaffRegistrationComponent } from './staff-registration/staff-registration.component';
import { MatSortModule } from '@angular/material/sort';
import { StaffProfileComponent } from './staff-profile/staff-profile.component';
import { EditStaffProfileComponent } from "./edit-staff-profile/edit-staff-profile.component";
import { BloodCenterEditComponent } from "./blood-center-edit/blood-center-edit.component";

const routes: Routes = [
  { path: 'register', component: RegistrationComponent },
  { path: 'donor-profile', component: DonorProfileComponent },
  { path: 'edit-donor-profile', component: EditDonorProfileComponent },
  { path: 'user-list', component: UserListComponent },
  { path: 'staff-register', component: StaffRegistrationComponent },
  { path: 'staff/profile', component: StaffProfileComponent },
  { path: 'staff/center', component: BloodCenterProfileComponent },
  { path: 'staff/edit-profile', component: EditStaffProfileComponent },
  { path: 'staff/edit-center', component: BloodCenterEditComponent },
  { path: 'center-register', component: CenterRegisterComponent },
  { path: 'center-list', component: CentersListComponent }


  
  
];

@NgModule({
  declarations: [
    BloodCenterProfileComponent,
    RegistrationComponent,
    CentersListComponent,
    DonorProfileComponent,
    BloodDonorFormComponent,
    CenterRegisterComponent,
    EditDonorProfileComponent,
    UserListComponent,
    StaffRegistrationComponent,
    StaffProfileComponent,
    EditStaffProfileComponent,
    BloodCenterEditComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    MatSortModule
  ],
  exports: [RouterModule,
    CentersListComponent,
    CenterRegisterComponent,
    BloodDonorFormComponent]
})

export class BloodBankModule { }
