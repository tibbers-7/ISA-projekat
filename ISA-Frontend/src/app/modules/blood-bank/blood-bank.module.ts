import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { MaterialModule } from "src/app/material/material.module";
import { BloodCenterProfileComponent } from "./blood-center-profile/blood-center-profile.component";

import { RegistrationComponent } from './registration/registration/registration.component';

const routes: Routes = [
  { path: 'blood-centers/k', component: BloodCenterProfileComponent}
  
];

@NgModule({
  declarations: [
    BloodCenterProfileComponent,
    RegistrationComponent
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
    RegistrationComponent
   ]
})

export class BloodBankModule { }
