import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from 'app/app-routing.module';
import { HomeComponent } from './home/home.component';
import { BloodBankModule } from '../blood-bank/blood-bank.module';
import { RouterModule, Routes } from "@angular/router";
import { DonorFormComponent } from './donor-form/donor-form/donor-form.component';
import { AdminNewCenterComponent } from './admin-new-center/admin-new-center/admin-new-center.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  {path:'form', component:DonorFormComponent}
];

@NgModule({
  declarations: [
    HomeComponent,
    DonorFormComponent,
    AdminNewCenterComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    BloodBankModule,
    RouterModule.forChild(routes)
  ]
})
export class PagesModule { }
