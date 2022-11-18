import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./modules/pages/home/home.component";
import { DonorFormComponent } from "./modules/pages/donor-form/donor-form/donor-form.component";
import { AdminNewCenterComponent } from "./modules/pages/admin-new-center/admin-new-center/admin-new-center.component";

const routes: Routes = [
  { path: '', component: HomeComponent },
  {path:'form', component:DonorFormComponent},
  { path: 'admin-register', component: AdminNewCenterComponent }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
