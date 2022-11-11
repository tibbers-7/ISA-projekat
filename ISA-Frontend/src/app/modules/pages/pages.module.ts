import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from 'app/app-routing.module';
import { HomeComponent } from './home/home.component';
import { BloodBankModule } from '../blood-bank/blood-bank.module';
import { RouterModule, Routes } from "@angular/router";


const routes: Routes = [
  { path: '', component: HomeComponent },
];
@NgModule({
  declarations: [
    HomeComponent,
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    BloodBankModule
  ]
})
export class PagesModule { }
