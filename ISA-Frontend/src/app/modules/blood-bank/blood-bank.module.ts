import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { MaterialModule } from "src/app/material/material.module";
import { BloodCenterProfileComponent } from "./blood-center-profile/blood-center-profile.component";
import { RegistrationComponent } from './registration/registration/registration.component';
import { CentersListComponent } from './centers-list/centers-list/centers-list.component';
import { BloodDonorFormComponent } from './bloodDonor-form/blood-donor-form/blood-donor-form.component';
import { UserProfileComponent } from './user-profile/user-profile.component';

const routes: Routes = [

  { path: 'blood-centers/k', component: BloodCenterProfileComponent},
  { path: 'register', component: RegistrationComponent },
  { path: 'user-profile/k', component: UserProfileComponent }

  
  
];

@NgModule({
  declarations: [
    BloodCenterProfileComponent,
    RegistrationComponent,
    CentersListComponent,
    UserProfileComponent,
    BloodDonorFormComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ],
  exports: [ 
    RouterModule,
    RegistrationComponent,
    CentersListComponent,
    UserProfileComponent
    CentersListComponent,
    BloodDonorFormComponent
   ]
})

export class BloodBankModule { }
