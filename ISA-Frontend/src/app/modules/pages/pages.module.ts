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


const routes: Routes = [
  { path: '', component: HomeComponent },
  {path:'register',component:RegistrationComponent},
  {path:'login',component:LoginComponent}
];

@NgModule({
  declarations: [
    HomeComponent,
    RegistrationComponent,
    LoginComponent,
    CentersListComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    RouterModule.forChild(routes),
    FormsModule,
    NgToastModule
  ],
  exports: [
    CentersListComponent
  ]
})
export class PagesModule { }
