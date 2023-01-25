import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from 'app/app-routing.module';
import { HomeComponent } from './home/home.component';
import { RouterModule, Routes } from "@angular/router";
import { RegistrationComponent } from './registration/registration.component';
import { FormsModule } from '@angular/forms';
import { NgToastModule } from 'ng-angular-popup';
import { LoginComponent } from './login/login.component';
import { CentersListComponent } from './centers-list/centers-list/centers-list.component';
import { MaterialModule } from 'app/material/material.module';
import { StartToolbarComponent } from './start-toolbar/start-toolbar/start-toolbar.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {path:'register',component:RegistrationComponent},
  {path:'login',component:LoginComponent},
  {path:'change-password',component:ChangePasswordComponent}
];

@NgModule({
  declarations: [
    HomeComponent,
    RegistrationComponent,
    LoginComponent,
    CentersListComponent,
    StartToolbarComponent,
    ChangePasswordComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    RouterModule.forChild(routes),
    FormsModule,
    NgToastModule,
    MaterialModule
  ],
  exports: [
    CentersListComponent
  ]
})
export class PagesModule { }
