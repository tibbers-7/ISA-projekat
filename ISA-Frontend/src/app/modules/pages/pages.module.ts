import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from 'app/app-routing.module';
import { HomeComponent } from './home/home.component';
import { BloodBankModule } from '../blood-bank/blood-bank.module';
import { RouterModule, Routes } from "@angular/router";
import { DonorFormComponent } from './donor-form/donor-form/donor-form.component';
import { AdminNewCenterComponent } from './admin-new-center/admin-new-center/admin-new-center.component';
import { RegistrationComponent } from './registration/registration.component';
import { FormsModule } from '@angular/forms';
import { Form } from '../blood-bank/model/form.model';
import { NgToastModule } from 'ng-angular-popup';
const routes: Routes = [
  { path: '', component: HomeComponent },
  {path:'form', component:DonorFormComponent},
  {path:'register',component:RegistrationComponent}
];

@NgModule({
  declarations: [
    HomeComponent,
    DonorFormComponent,
    AdminNewCenterComponent,
    RegistrationComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    BloodBankModule,
    RouterModule.forChild(routes),
    FormsModule,
    NgToastModule
  ]
})
export class PagesModule { }
