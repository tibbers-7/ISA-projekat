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
import { TestprobaComponent } from './testproba/testproba.component';
import { GoogleMapsModule } from "@angular/google-maps";
import {GoogleMap} from "@angular/google-maps"
import { MapsModule } from '@syncfusion/ej2-angular-maps';
import { LegendService, MarkerService, MapsTooltipService, DataLabelService, BubbleService, NavigationLineService, SelectionService, AnnotationsService, ZoomService } from '@syncfusion/ej2-angular-maps';



const routes: Routes = [
  { path: '', component: HomeComponent },
  {path:'register',component:RegistrationComponent},
  {path:'login',component:LoginComponent},
  {path:'proba',component:TestprobaComponent}
];

@NgModule({
  declarations: [
    HomeComponent,
    RegistrationComponent,
    LoginComponent,
    CentersListComponent,
    StartToolbarComponent,
    TestprobaComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    RouterModule.forChild(routes),
    FormsModule,
    NgToastModule,
    MaterialModule,
    GoogleMapsModule,
    MapsModule
  ],
  exports: [
    CentersListComponent
  ],
  providers: [
    GoogleMap,
    LegendService, 
    MarkerService, 
    MapsTooltipService, 
    DataLabelService, 
    BubbleService, 
    NavigationLineService , 
    SelectionService, 
    AnnotationsService, 
    ZoomService
  ]
})
export class PagesModule { }
