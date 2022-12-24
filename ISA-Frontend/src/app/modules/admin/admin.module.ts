import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminHomepageComponent } from './admin-homepage/admin-homepage.component';
import { AdminToolbarComponent } from './admin-toolbar/admin-toolbar.component';
import { MaterialModule } from 'app/material/material.module';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { RoleGuardService } from 'app/auth/role-guard.service';
import { AdminNewCenterComponent } from './admin-new-center/admin-new-center/admin-new-center.component';
import { UserListComponent } from './user-list/user-list.component';
import { StaffRegistrationComponent } from './staff-registration/staff-registration.component';
import { BloodBankModule } from '../blood-bank/blood-bank.module';

const routes: Routes = [

  { path: 'admin/homepage', component:AdminHomepageComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' }},
  { path: 'admin/user-list', component: UserListComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' } },
  {path: 'admin/new-center', component:AdminNewCenterComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' }},
  { path: 'admin/staff-register', component: StaffRegistrationComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' } },

];
@NgModule({
  declarations: [
    AdminHomepageComponent,
    AdminToolbarComponent,
    UserListComponent,
    AdminNewCenterComponent,
    StaffRegistrationComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    BloodBankModule
  ],
  exports: [
    AdminToolbarComponent
  ]
})
export class AdminModule { }
