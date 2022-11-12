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
import {MatSortModule} from '@angular/material/sort';

const routes: Routes = [

  { path: 'blood-centers/k', component: BloodCenterProfileComponent},
  { path: 'register', component: RegistrationComponent },
  { path: 'user-profile/:id', component: UserProfileComponent },
  { path: 'edit-user-profile/:id', component: EditUserProfileComponent },
  { path: 'user-list', component: UserListComponent }

  
  
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
    UserListComponent
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
    CenterRegisterComponent
   ]
})

export class BloodBankModule { }
