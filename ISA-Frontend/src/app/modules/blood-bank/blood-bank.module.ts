import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { MaterialModule } from "src/app/material/material.module";
import { BloodCenterProfileComponent } from "./blood-center-profile/blood-center-profile.component";
import { RegistrationComponent } from './registration/registration/registration.component';
import { CentersListComponent } from './centers-list/centers-list/centers-list.component';

const routes: Routes = [

  { path: 'blood-centers/k', component: BloodCenterProfileComponent},
  {path:'register',component:RegistrationComponent}

  
  
];

@NgModule({
  declarations: [
    BloodCenterProfileComponent,
    RegistrationComponent,
    CentersListComponent
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
    CentersListComponent
   ]
})

export class BloodBankModule { }
